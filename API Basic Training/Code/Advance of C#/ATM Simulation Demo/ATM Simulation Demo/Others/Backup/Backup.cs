using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

public class BackupService
{

    private readonly string _backupPath = "F:\\367 - Prince.G\\git\\API Basic Training\\Code\\Advance of C#\\ATM Simulation Demo\\ATM Simulation Demo\\Backup\\backup.json";
    private readonly BackupRepository _backupRepository;

    #region Constructor
    public BackupService()
    {
        _backupRepository = new BackupRepository();
    }
    #endregion

    public string BackupData()
    {
        try
        {

            string backupData = _backupRepository.GetBackupData();
            // Check if the file exists
            if (!File.Exists(_backupPath))
            {
                // Create the file
                File.Create(_backupPath).Close();
            }

            // Write JSON to file
            File.WriteAllText(_backupPath, backupData);
            return _backupPath;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            throw ex;
        }
    }

}