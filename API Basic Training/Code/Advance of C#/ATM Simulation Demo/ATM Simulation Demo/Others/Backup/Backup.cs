using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models.POCO;
using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;

public class BackupService
{
    private readonly string _backupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Backup/backup.json");
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
            //// get data set
            DataSet backupData = _backupRepository.GetBackupData();

            string JSONobject = JsonConvert.SerializeObject(backupData);

            // Check if the file exists
            if (!File.Exists(_backupPath))
            {
                // Create the file
                File.Create(_backupPath).Close();
            }

            // Write JSON to file
            File.WriteAllText(_backupPath, JSONobject);
            return _backupPath;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            throw ex;
        }
    }

}