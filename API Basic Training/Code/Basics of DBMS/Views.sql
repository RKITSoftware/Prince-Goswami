-- Create a view that combines information from BNK01 and BRC01
CREATE VIEW VWS_BankBranch AS
SELECT BRC01.C01F01 as BranchID, BRC01.C01F02 as BranchName, BRC01.C01F03 as IFSCCode, BNK01.K01F02 as BankName
FROM BRC01
INNER JOIN BNK01 ON BRC01.C01F04 = BNK01.K01F01;

-- Drop a view
DROP VIEW VWS_BankBranch ;

-- Create an index on the BankName column in the view
CREATE INDEX idx_BankBranchView_BankName ON BankBranchView(BankName);
