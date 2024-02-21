using System;
using System.Collections.Generic;
using System.IO;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

public class BackupService
{

    private readonly string _backupPath = "F:\\367 - Prince.G\\git\\API Basic Training\\Code\\Advance of C#\\ATM Simulation Demo\\ATM Simulation Demo\\Backup\\backup.json"; // Adjust the path accordingly

    public string BackupData(List<ACC01> accounts, List<TRN01> allTransactions, List<USR01> users)
    {
        try
        {

            BackupDataModel backupData = new BackupDataModel
            {
                Accounts = accounts,
                AllTransactions = allTransactions,
                Users = users
            };

            // Check if the file exists
            if (!File.Exists(_backupPath))
            {
                // Create the file
                File.Create(_backupPath).Close();
            }

            // Serialize data to JSON
            string jsonBackup = JsonConvert.SerializeObject(backupData, Formatting.Indented);

            // Write JSON to file
            File.WriteAllText(_backupPath, jsonBackup);
            return _backupPath;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            throw ex;
       }
    }

}