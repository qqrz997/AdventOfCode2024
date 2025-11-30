using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Common;

public static class ProblemInputs
{
    public static Input GetInput([CallerFilePath] string callerPath = "")
    {
        var callerDirectory = Path.GetDirectoryName(callerPath) ?? throw new DirectoryNotFoundException();
        var inputFile = new FileInfo(Path.Combine(callerDirectory, "input.txt"));
        
        string input;
        do
        {
            if (!inputFile.Exists)
            {
                inputFile.Create().Close();
            }
            
            Console.WriteLine("Opening input file. Enter the puzzle input, then save and close the file to continue.");
            
            using var notepadProcess = Process.Start("notepad.exe", inputFile.FullName);
            notepadProcess.WaitForExit();

            using var reader = inputFile.OpenText();
            input = reader.ReadToEnd();
        } while (string.IsNullOrEmpty(input));
        
        return new(input);
    }
}