ALTER TABLE Number
DROP CONSTRAINT PK_Number;

ALTER TABLE Number
ALTER COLUMN LotNumber nvarchar(200)

ALTER TABLE Number
ADD CONSTRAINT PK_Number PRIMARY KEY (NumberID);