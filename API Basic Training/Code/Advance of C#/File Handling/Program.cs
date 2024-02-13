using System;
using System.IO;
using System.Text;

/// <summary>
/// A C# console application demonstrating advanced handling of the file system.
/// Uses Directory, File, FileSystemInfo, and Path classes for various file operations.
/// </summary>
class Program
{
    static void Main()
    {
        string rootPath = @"F:\367 - Prince.G\git\API Basic Training\Code\Advance of C#\File Handling\Files"; // Replace with your actual root path

        // List all files and directories in the root path
        ListFilesAndDirectories(rootPath);

        // Create a new file
        string newFileName = "sample.txt";
        string newFilePath = Path.Combine(rootPath, newFileName);
        CreateFile(newFilePath, "This is a new file content.");

        // Copy a file from one location to another
        CopyFile(rootPath, "sample.txt", "Backup");

        // Get information about a specific file
        GetFileInfo(rootPath, "sample.txt");


        // Read content from the new file
        ReadFile(newFilePath);

        // Update content in the new file
        UpdateFile(newFilePath, "Updated content for the new file.");

        // Read content from the updated file
        ReadFile(newFilePath);

        // Delete the new file
        DeleteFile(newFilePath);

        Console.ReadKey();
    }


    #region List Files and Directories

    /// <summary>
    /// Lists all files and directories in the specified path.
    /// </summary>
    static void ListFilesAndDirectories(string path)
    {
        Console.WriteLine($"Listing files and directories in: {path}");

        // List directories
        string[] directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            Console.WriteLine($"Directory: {directory}");
        }

        // List files
        string[] files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            Console.WriteLine($"File: {file}");
        }
    }
    #endregion

    #region Copy File

    /// <summary>
    /// Copies a file from one location to another.
    /// </summary>
    static void CopyFile(string rootPath, string fileName, string destinationFolder)
    {
        string sourceFilePath = Path.Combine(rootPath, fileName);
        string destinationFolderPath = Path.Combine(rootPath, destinationFolder);

        // Ensure the destination folder exists
        if (!Directory.Exists(destinationFolderPath))
        {
            Directory.CreateDirectory(destinationFolderPath);
        }

        string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

        // Copy the file
        File.Copy(sourceFilePath, destinationFilePath, true);
        Console.WriteLine($"File copied to: {destinationFilePath}");
    }

    #endregion

    #region Get File Info

    /// <summary>
    /// Retrieves information about a specific file.
    /// </summary>
    static void GetFileInfo(string rootPath, string fileName)
    {
        string filePath = Path.Combine(rootPath, fileName);

        // Get information about the file
        FileInfo fileInfo = new FileInfo(filePath);

        Console.WriteLine($"File Information for: {fileName}");
        Console.WriteLine($"Full Path: {fileInfo.FullName}");
        Console.WriteLine($"Size: {fileInfo.Length} bytes");
        Console.WriteLine($"Created: {fileInfo.CreationTime}");
        Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
    }

    #endregion

    #region Create, Read, Update, and Delete File

    /// <summary>
    /// Creates a new file with the specified content.
    /// </summary>
    static void CreateFile(string filePath, string content)
    {
        // Write content to a new file
        File.WriteAllText(filePath, content, Encoding.UTF8);
        Console.WriteLine($"File created at: {filePath}");
    }

    /// <summary>
    /// Reads and displays the content of a file.
    /// </summary>
    static void ReadFile(string filePath)
    {
        // Read content from the file
        string content = File.ReadAllText(filePath);
        Console.WriteLine($"File Content:\n{content}");
    }


    /// <summary>
    /// Updates the content of an existing file.
    /// </summary>
    static void UpdateFile(string filePath, string newContent)
    {
        // Update content in the file
        File.WriteAllText(filePath, newContent, Encoding.UTF8);
        Console.WriteLine($"File updated at: {filePath}");
    }

    /// <summary>
    /// Deletes a file.
    /// </summary>
    static void DeleteFile(string filePath)
    {
        // Delete the file
        File.Delete(filePath);
        Console.WriteLine($"File deleted: {filePath}");
    }
    #endregion

}
