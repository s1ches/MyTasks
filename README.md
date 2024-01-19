# MyTasks
Repository for my tasks from anywhere

## 1. MicrosoftDocsParsing
Getting to know and working with the DocumentFormat.OpenXml library, the application parses a doc or docx file and takes only human-visible characters, and the characters that are formed as a result of encoding are ignored (file encoding is done to bypass the anti-plagiarism check)

##### For start the application u need to create a folder Files and 2 folders into this one: UnparsedFiles, ParsedFiles, after that u need to put the files with extension .doc or .docx in UnparsedFiles folder and run the application
###### P.S. 
Don't use method ProcesAllUnparsedFilesAsync it's not work on big count of unparsed files
What u can do?
1. U can finalize it correctly
2. U can rewrite it to synchronously files processing

## 2. NotesApi

Finally I've done this project by this YouTube Course: https://www.youtube.com/playlist?list=PLEtg-LdqEKXbpq4RtUp1hxZ6ByGjnvQs4

So, what can I say about this work, it's very interesting project for me, because this one have a lot of new technologies, patterns for me, like
- I became familiar with the concept of **Clean Architecture**
- Pattern **CQRS + MediatR** (Library)
- **Fluent Validation**
- **Identity Server4**
- **OAuth2.0 and OIDC** (OpenID Connect)
- **CORS**
- **Serilog**
- **AutoMapper**

And now I need realize my own project, using this technologies
I think, that it can be Simple chat with using ~~SignalR~~, ~~TypeScript+React~~ and other technologies, which I use in this project

#### If u wanna try to start my app, u need create front-end part, or delete [Authorize] attributes from controller methods:)

###### P.S. 

Project structure must be like that, but my file system decided to joke with me<br>
![image](https://github.com/s1ches/MyTasks/assets/121990701/7fa828dd-f8a8-4461-a0cd-653c4404cb81)
