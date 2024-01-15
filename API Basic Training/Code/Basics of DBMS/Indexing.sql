
-- Single-column index on BankName in BNK01
CREATE INDEX idx_BankName ON BNK01(K01F02);

-- Single-column index on IFSCCode in BRC01
CREATE INDEX idx_IFSCCode ON BRC01(C01F03);

-- Composite index on BranchName and IFSCCode in BRC01
CREATE INDEX idx_BranchName_IFSCCode ON BRC01(C01F02, C01F03);

-- Foreign key index on BankID in BRC01 referencing BNK01
ALTER TABLE BRC01
ADD INDEX idx_BankID (C01F04),
ADD FOREIGN KEY (C01F04) REFERENCES BNK01(K01F01);