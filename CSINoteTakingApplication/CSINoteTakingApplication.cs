//https://www.thecodingguys.net/blog/csharp-create-command-line-notes-application

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace CSINoteTakingApplication
{
    class Program //Main class
    {

        static void Main(string[] args)// Main method
        {
            ReadCommand();
            Console.ReadLine();
        }


        //Store our notes directory, environment variable, get folder path for your mydoc folder and the notes folder
        private static string NoteDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Notes\";

        private static void ReadCommand()//method to read user input as command
        {
            //Read user input - Then execute correct method..
            Console.Write(Directory.GetDirectoryRoot(NoteDirectory));
            string Command = Console.ReadLine();//reads user input

            switch (Command.ToLower())//takes user input and decides which command below to execute
            {
                case "new":
                    NewNote();
                    Main(null);
                    break;
                case "edit":
                    EditNote();
                    Main(null);
                    break;
                case "read":
                    ReadNote();
                    Main(null);
                    break;
                case "delete":
                    DeleteNote();
                    Main(null);
                    break;
                case "shownotes":
                    ShowNotes();
                    Main(null);
                    break;
                case "dir":
                    NotesDirectory();
                    Main(null);
                    break;
                case "cls":
                    Console.Clear();
                    Main(null);
                    break;
                case "exit":
                    Exit();
                    break;
                default:
                    CommandsAvailable();
                    Main(null);
                    break;
            }
        }

        private static void NewNote()//method for new note
        {

            Console.WriteLine("Please Enter Note:\n");//output to show user what to do
            string input = Console.ReadLine();//Read user input

            XmlWriterSettings NoteSettings = new XmlWriterSettings();//Add XML settings, change as you wish..

            NoteSettings.CheckCharacters = false;//class so characters are not checked for xml
            NoteSettings.ConformanceLevel = ConformanceLevel.Auto;//"reader" decides what doc type based on data input
            NoteSettings.Indent = true;

            string FileName = DateTime.Now.ToString("dd-MM-yy") + ".xml";//The file name in date format..

            //write the file..
            using (XmlWriter NewNote = XmlWriter.Create(NoteDirectory + FileName, NoteSettings))
            {
                NewNote.WriteStartDocument();
                NewNote.WriteStartElement("Note");
                NewNote.WriteElementString("body", input);
                NewNote.WriteEndElement();

                NewNote.Flush();
                NewNote.Close();
            }
        }

        private static void EditNote()//to edit an existing note
        {
            Console.WriteLine("Please enter file name.\n");//get user input of file to edit
            string FileName = Console.ReadLine().ToLower();//Read user input

            if (File.Exists(NoteDirectory + FileName))//if else statement for user input
            {

                XmlDocument doc = new XmlDocument();//new variable

                //Load the document
                try//try loop
                {
                    doc.Load(NoteDirectory + FileName);//load document

                    Console.Write(doc.SelectSingleNode("//body").InnerText);//get the note stored

                    string ReadInput = Console.ReadLine();

                    if (ReadInput.ToLower() == "cancel")
                    {
                        Main(null);
                    }
                    else
                    {
                        string newText = doc.SelectSingleNode("//body").InnerText = ReadInput;
                        doc.Save(NoteDirectory + FileName);
                    }
                }
                catch (Exception ex)//catch error so it isn't an infinate loop
                {
                    Console.WriteLine("Could not edit note following error occurred: " + ex.Message);//output if there is an error
                }


            }
            else
            {
                Console.WriteLine("File not found\n");//if there is no file by the name of input
            }

        }

        private static void ReadNote()//to display note
        {
            Console.WriteLine("Please enter file name.\n");//get user input
            string FileName = Console.ReadLine().ToLower();//takes user input

            if (File.Exists(NoteDirectory + FileName))//if else statement
            {
                XmlDocument Doc = new XmlDocument();
                Doc.Load(NoteDirectory + FileName);

                Console.WriteLine(Doc.SelectSingleNode("//body").InnerText);//displays body of note
            }
            else
            {
                Console.WriteLine("File not found");//if no file by user input name
            }



        }

        private static void DeleteNote()//delete's note
        {
            Console.WriteLine("Please enter file name\n");//gets user input
            string FileName = Console.ReadLine();//reads user input

            if (File.Exists(NoteDirectory + FileName))//if else statement
            {
                Console.WriteLine(Environment.NewLine + "Are you sure you wish to delete this file? Y/N\n");//confirmation statement to user
                string Confirmation = Console.ReadLine().ToLower();//reads input

                if (Confirmation == "y")//if else for y or n
                {
                    try//try catch to find document and delete
                    {
                        File.Delete(NoteDirectory + FileName);
                        Console.WriteLine("File has been deleted\n");
                    }
                    catch (Exception ex)//catch to prevent infinite loop, display error message
                    {
                        Console.WriteLine("File not deleted following error occured: " + ex.Message);
                    }
                }
                else if (Confirmation == "n")//does not delete file, makes it a null statement
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("Invalid command\n");//if something other than y or n is entered this is printed
                    DeleteNote();
                }
            }
            else
            {
                Console.WriteLine("File does not exist\n");//if no file by user input
                DeleteNote();
            }
        }

        private static void ShowNotes()//shows directory location
        {
            string NoteLocation = NoteDirectory;//makes notelocation string

            DirectoryInfo Dir = new DirectoryInfo(NoteLocation);//makes dir note location

            if (Directory.Exists(NoteLocation))//if else statement to show location
            {
                FileInfo[] NoteFiles = Dir.GetFiles("*.xml");//adds .xml to user input

                if (NoteFiles.Count() != 0)//shows location and puts the cursor in position, displays output
                {

                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop + 2);
                    Console.WriteLine("+------------+");
                    foreach (var item in NoteFiles)
                    {
                        Console.WriteLine("  " + item.Name);
                    }



                    Console.WriteLine(Environment.NewLine);//makes new line
                }
                else
                {
                    Console.WriteLine("No notes found.\n");//if not found
                }

            }
            else
            {//if file is not in directory the directory is created
                Console.WriteLine(" Directory does not exist.....creating directory\n");
                Directory.CreateDirectory(NoteLocation);
                Console.WriteLine(" Directory: " + NoteLocation + " created successfully.\n");
            }
        }

        private static void Exit()//exits methods
        {
            Environment.Exit(0);
        }

        private static void CommandsAvailable()//shows option for user input for next round
        {
            Console.WriteLine(" New - Create a new note\n Edit - Edit a note\n Read -  Read a note\n ShowNotes - List all notes\n Exit - Exit the application\n Dir - Opens note directory\n Help - Shows this help message\n");
        }

        private static void NotesDirectory()//starts explorer.exe to be able to perform program
        {
            Process.Start("explorer.exe", NoteDirectory);
        }

    }
}
