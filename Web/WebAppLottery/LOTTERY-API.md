# Lottery API và kết nối ChatGPT

API này đọc bảng Numbers của cơ sở dữ liệu ứng dụng hiện tại. Không cần cài thêm ASP.NET Web API; controller MVC 5 trả JSON qua Newtonsoft.Json. Các URL bên dưới chỉ có hiệu lực sau khi build và deploy.

## Toàn bộ dữ liệu cả bốn loại vé

GET https://vietnamlott.net/api/v1/lottery/all

Trả toàn bộ lịch sử đang lưu cho 645, 655, 3dmax, 3dmaxpro trong games; fullHistory=true. Không lọc ngày, không phân trang, không giới hạn số kỳ. Mỗi loại luôn có một phần tử, kể cả draws rỗng. API không tự bổ sung dữ liệu chưa có trong database.

GPT Actions có giới hạn kích thước response, nên schema Action dùng endpoint theo loại vé để tải tất cả các trang của cả 4 loại. Phân trang chỉ phục vụ vận chuyển dữ liệu; phạm vi phân tích vẫn là toàn bộ lịch sử. Endpoint /all dành cho tải JSON trực tiếp hoặc ứng dụng đọc được response lớn.

## Endpoint

GET /api/v1/lottery/{game}/draws

| game | Loại vé |
| --- | --- |
| 645 | Mega 6/45 |
| 655 | Power 6/55 |
| 3dmax | Max 3D |
| 3dmaxpro | Max 3D Pro |

Ví dụ (đổi domain nếu deploy ở nơi khác):

- https://vietnamlott.net/api/v1/lottery/645/draws?limit=50
- https://vietnamlott.net/api/v1/lottery/655/draws?limit=1
- https://vietnamlott.net/api/v1/lottery/3dmax/draws?from=2026-01-01&to=2026-10-07&limit=50
- https://vietnamlott.net/api/v1/lottery/3dmaxpro/draws?limit=50&offset=50

from/to: ngày bao gồm hai đầu, định dạng yyyy-MM-dd. Không truyền thì đọc mọi ngày. Bỏ limit thì trả toàn bộ lịch sử (limit=null trong response); truyền limit từ 1 đến 100 để phân trang vận chuyển. offset mặc định 0. Sắp xếp ngày mới nhất trước. nextOffset chỉ vị trí trang tiếp theo, null khi hết dữ liệu. totalDraws là số ngày khớp bộ lọc. Một kỳ được nhóm theo ngày và loại vé, phù hợp cấu trúc hiện có.

HTTP 400: tham số không hợp lệ; 401: thiếu/sai Bearer key khi bật; 503: không đọc được database. Không đưa lỗi kết nối hay mật khẩu database vào response.

## Ý nghĩa dữ liệu

- source=local_database. GET không gọi scraper và không ghi database. Tiếp tục chạy luồng cập nhật dữ liệu hiện tại; API không sửa lỗi scraper Parsed=0.
- latestAvailableDrawDate là ngày kỳ mới nhất đang lưu của loại vé, kể cả ngoài khoảng lọc; fetchedAtUtc là thời điểm đọc API, không phải lúc đồng bộ Vietlott.
- date dùng yyyy-MM-dd; timezone=Asia/Ho_Chi_Minh. drawNumber là KyQuay nếu có, nếu không thì null.
- prizes: levelId 1=special (đặc biệt), 2=first (nhất), 3=second (nhì), 4=third (ba).
- numbers luôn là chuỗi: 6/45, 6/55 có hai chữ số; 3D có ba chữ số, giữ 007. Chuỗi ghép của 3D được tách thành từng số theo thứ tự lưu trong mỗi giải. Không loại số trùng vì có thể làm sai dữ liệu gốc.
- warnings báo bản ghi không hợp lệ, số lượng thiếu/thừa và hạn chế cấu trúc dữ liệu. Số không hợp lệ bị bỏ khỏi numbers, đồng thời có cảnh báo. Không dùng kỳ có cảnh báo dữ liệu lỗi như một kỳ đầy đủ.
- 6/55: database cũ không đánh dấu main/bonus. API trả cả 7 số theo NumberId và cảnh báo; không được coi toàn bộ 7 số là 7 số chính, cũng không mặc định số cuối là số đặc biệt. Muốn phân tích riêng 6 số chính cần bổ sung trường vai trò và đối chiếu dữ liệu nguồn trước.
- 3D Max Pro: giữ thứ tự trong giải, chưa có nhãn vai trò/cặp; không tự gán vai trò khi dữ liệu gốc không xác định.

## Deploy

1. Build/publish WebAppLottery bằng Visual Studio hoặc MSBuild trên máy có .NET Framework 4.8 và Web Application targets. File api-docs/openapi.json đã được thêm vào project để publish.
2. Dùng connection string hiện có; kiểm tra API trả dữ liệu trên IIS. Không truy cập /Home/Data để smoke test API vì trang đó chạy cập nhật dữ liệu.
3. Bật HTTPS trên port 443, chứng chỉ công khai hợp lệ và TLS 1.2 trở lên. GPT Actions yêu cầu HTTPS. Không dùng localhost hoặc chứng chỉ tự ký cho kết nối từ ChatGPT.
4. Nếu khác domain hoặc deploy trong thư mục con, sửa servers[0].url trong api-docs/openapi.json thành base URL HTTPS thực tế (bao gồm thư mục con, không thêm /api/v1).
5. Test cả bốn loại vé với limit=1; kiểm tra latestAvailableDrawDate và warnings bằng dữ liệu thật. Đảm bảo database có bản ghi mới; API không tự tạo dữ liệu.

## Kết nối Custom GPT qua Actions

GPT Actions cho phép Custom GPT gọi REST API bằng OpenAPI schema:
https://developers.openai.com/api/docs/actions/introduction

1. Trong trình cấu hình Custom GPT, thêm Action.
2. Import schema từ https://vietnamlott.net/api/v1/lottery/openapi (hoặc dán nội dung api-docs/openapi.json).
3. Mặc định API đọc công khai: chọn Authentication=None. Nếu muốn giới hạn truy cập, thêm appSettings LotteryApiKey với một khóa ngẫu nhiên riêng trên server; chọn API Key/Bearer trong Action và nhập cùng khóa. Không dùng khóa OpenAI làm khóa LotteryApiKey.
4. Test getLotteryDraws với game=645, limit=1, sau đó các game khác.
5. Dán hướng dẫn ở mục dưới vào Instructions của GPT.

Nếu bật Bearer, thêm securitySchemes và security sau vào schema Action:

    "components": {
      "securitySchemes": {
        "LotteryBearer": { "type": "http", "scheme": "bearer" }
      },
      "schemas": { "...": "giữ nguyên schemas hiện có" }
    },
    "security": [{ "LotteryBearer": [] }]

Ví dụ cấu hình riêng trong Web.config trên server (thay giá trị mẫu bằng khóa thật):

    <appSettings>
      <!-- Giữ nguyên các appSettings đang có -->
      <add key="LotteryApiKey" value="YOUR_RANDOM_PRIVATE_KEY" />
    </appSettings>

Endpoint schema vẫn công khai, không chứa khóa. Khóa phải nằm trong cấu hình server/Action, không trong URL hay tài liệu. Chỉ các endpoint đọc lịch sử này được mô tả trong Action.

Yêu cầu HTTPS, timeout 45 giây và response dưới 100.000 ký tự:
https://developers.openai.com/api/docs/actions/production

Việc có API không tự kết nối mọi cuộc chat. Cần dùng GPT đã cấu hình Action; hội thoại hiện tại chưa được nối tự động với endpoint mới.

## Instructions gợi ý cho Custom GPT

Bạn phân tích toàn bộ dữ liệu lịch sử Vietlott của cả 4 loại vé bằng getLotteryDraws. Với mỗi yêu cầu phân tích, đọc lần lượt game=645, 655, 3dmax, 3dmaxpro. Không truyền from/to để cắt lịch sử. Truyền limit=50 và offset=0, sau đó tiếp tục đúng nextOffset của từng game đến khi hasMore=false. 50 chỉ là kích thước trang, không phải phạm vi dữ liệu. Không dừng ở trang đầu hoặc tự đổi sang 50 kỳ gần nhất. Chỉ kết luận đã lấy đầy đủ khi số kỳ nhận được của từng game khớp totalDraws. Nếu bị giới hạn công cụ, thời gian, ngữ cảnh hoặc có lỗi giữa chừng, báo rõ chưa đủ dữ liệu, không khẳng định đã phân tích full. Báo khoảng ngày, số kỳ và ngày kỳ mới nhất cho từng loại. Nếu dữ liệu thay đổi giữa các trang và số kỳ không khớp, đọc lại để đối chiếu.

Luôn kiểm tra warnings, latestAvailableDrawDate và draws. Nếu API lỗi, dữ liệu rỗng hoặc cũ, nói rõ; không bịa kết quả và không gọi fetchedAtUtc là ngày kỳ quay. Loại kỳ thiếu/không hợp lệ khỏi thống kê và công bố số kỳ bị loại. Giữ số 0 đầu của số 3D. Phân tích từng giải riêng. Với 6/55, không đoán vai trò số đặc biệt từ thứ tự lưu; chỉ phân tích cả bảy số với giới hạn này hoặc yêu cầu dữ liệu có vai trò rõ ràng. Không tự suy diễn cặp/vai trò Max 3D Pro.

Khi đề xuất số tham khảo, mô tả phương pháp (tần suất, khoảng vắng, cửa sổ lịch sử) và giới hạn dữ liệu. Không khẳng định xác suất thắng tăng vì số nóng/lạnh hoặc kết quả gần đây. Các kỳ quay ngẫu nhiên không được dự đoán chắc chắn từ lịch sử.

Ví dụ yêu cầu: “Lấy toàn bộ lịch sử cả 4 loại vé, thống kê riêng từng loại và từng giải, rồi đề xuất số tham khảo kèm phương pháp.”

## Kiểm tra phát triển

Chạy api-tests/Run-Tests.ps1 từ PowerShell để biên dịch và chạy test định dạng/cảnh báo/HTTP 400, không gọi database hoặc scraper. Trong phiên triển khai ban đầu, toàn bộ 17 file C# đã biên dịch thành công với các DLL hiện có; còn bốn cảnh báo cũ CS0472 trong HomeController. Chưa kiểm tra HTTP trên IIS/database thật hoặc kết nối Custom GPT.
