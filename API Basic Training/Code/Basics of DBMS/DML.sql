-- DML statements are used to manage and manipulate data stored in the database.

-- INSERT
INSERT INTO BNK01
(
	K01F02,
    K01F03
)
VALUES(
	"Bank of Baroda",
    "BOB"
);

-- UPDATE

UPDATE BNK01
SET
	K01F02 = "Bank Of Baroda"
WHERE(
	K01F01 = 3
 );
 
-- SELECT

SELECT K01F02, K01F03 FROM BNK01;

-- DELETE

DELETE FROM 
	BNK01
WHERE 
	K01F01 = 1 OR K01F01 = 2