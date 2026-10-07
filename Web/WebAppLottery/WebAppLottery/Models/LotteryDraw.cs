using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LotteryDAL;

namespace WebAppLottery.Models
{
    public class LotteryDraw
    {
        public string date { get; set; }
        public int? drawNumber { get; set; }
        public List<LotteryPrize> prizes { get; set; }
        public List<string> warnings { get; set; }
    }
    public class LotteryPrize
    {
        public int levelId { get; set; }
        public string level { get; set; }
        public List<string> numbers { get; set; }
    }
    public class LotteryGameHistory
    {
        public string game { get; set; }
        public int totalDraws { get; set; }
        public string latestAvailableDrawDate { get; set; }
        public List<LotteryDraw> draws { get; set; }
    }
    public static class LotteryDrawFormatter
    {
        public static List<LotteryGameHistory> FormatAll(IList<Number> rows)
        {
            var types = new short[] { (short)Enum_NumberType._6Over45, (short)Enum_NumberType._6Over55,
                (short)Enum_NumberType._3DMax, (short)Enum_NumberType._3DMaxPro };
            var names = new[] { "645", "655", "3dmax", "3dmaxpro" };
            var byType = rows.ToLookup(x => x.NumberTypeId);
            return types.Select((type, index) =>
            {
                var draws = byType[type].GroupBy(x => x.DatePublish.Date).OrderByDescending(x => x.Key)
                    .Select(x => Format(names[index], x.Key, x.ToList())).ToList();
                return new LotteryGameHistory { game = names[index], totalDraws = draws.Count,
                    latestAvailableDrawDate = draws.Count == 0 ? null : draws[0].date, draws = draws };
            }).ToList();
        }
        public static LotteryDraw Format(string game, DateTime date, IList<Number> rows)
        {
            var result = new LotteryDraw { date = date.ToString("yyyy-MM-dd"),
                prizes = new List<LotteryPrize>(), warnings = new List<string>() };
            var drawNumbers = rows.Where(x => x.KyQuay.HasValue).Select(x => x.KyQuay.Value).Distinct().ToList();
            result.drawNumber = drawNumbers.Count == 1 ? (int?)drawNumbers[0] : null;
            if (drawNumbers.Count > 1) result.warnings.Add("Multiple draw identifiers on one date.");
            bool threeDigit = game == "3dmax" || game == "3dmaxpro";
            foreach (var group in rows.OrderBy(x => x.NumberId).GroupBy(x => x.NumberWinLevelId).OrderBy(x => x.Key))
            {
                var prize = new LotteryPrize { levelId = group.Key, level = LevelName(group.Key), numbers = new List<string>() };
                foreach (var row in group)
                {
                    var value = (row.LotNumber ?? "").Trim();
                    if (threeDigit)
                    {
                        if (value.Length == 0 || value.Length % 3 != 0 || value.Any(c => c < '0' || c > '9'))
                        { result.warnings.Add("Invalid 3-digit data at record " + row.NumberId); continue; }
                        for (int i = 0; i < value.Length; i += 3) prize.numbers.Add(value.Substring(i, 3));
                    }
                    else
                    {
                        int number;
                        if (!int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out number) || number < 1 || number > (game == "645" ? 45 : 55))
                        { result.warnings.Add("Invalid number at record " + row.NumberId); continue; }
                        prize.numbers.Add(number.ToString("D2", CultureInfo.InvariantCulture));
                    }
                }
                result.prizes.Add(prize);
            }
            if (threeDigit)
            {
                for (int level = 1; level <= 4; level++)
                {
                    var prize = result.prizes.FirstOrDefault(x => x.levelId == level);
                    if (prize == null || prize.numbers.Count != level * 2)
                        result.warnings.Add("Missing or unexpected number count for prize level " + level);
                }
            }
            else
            {
                var numbers = result.prizes.SelectMany(x => x.numbers).ToList();
                if (numbers.Count != (game == "645" ? 6 : 7)) result.warnings.Add("Incomplete or duplicate draw records.");
                if (numbers.Distinct().Count() != numbers.Count) result.warnings.Add("Duplicate numbers in draw.");
            }
            if (game == "655") result.warnings.Add("Legacy storage has no main/bonus flag. Seven numbers retain record order; do not infer which is the bonus number.");
            if (game == "3dmaxpro") result.warnings.Add("Numbers retain stored order within each prize; pair roles are not labeled in legacy storage.");
            return result;
        }
        private static string LevelName(short level)
        {
            switch (level)
            {
                case 1: return "special";
                case 2: return "first";
                case 3: return "second";
                case 4: return "third";
                default: return "level_" + level;
            }
        }
    }
}
