# MyTasks
Repository for my tasks from anywhere

## 1. MicrosoftDocsParsing
Getting to know and working with the DocumentFormat.OpenXml library, the application parses a doc or docx file and takes only human-visible characters, and the characters that are formed as a result of encoding are ignored (file encoding is done to bypass the anti-plagiarism check)

##### For start the application u need to create a folder Files and 2 folders into this one: UnparsedFiles, ParsedFiles, after that u need to put the files with extension .doc or .docx in UnparsedFiles folder and run the application
####### P.S. 
Don't use method ProcesAllUnparsedFilesAsync it's not work on big count of unparsed files
What u can do?
1. U can finalize it correctly
2. U can rewrite it to synchronously files processing
