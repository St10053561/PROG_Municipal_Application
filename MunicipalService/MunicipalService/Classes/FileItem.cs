namespace MunicipalService.Classes
{
    public class FileItem
    {
        // Property to store the file path
        public string FilePath { get; set; }

        // Property to store the file name
        public string FileName { get; set; }

        // Property to indicate if the file is an image
        public bool IsImage { get; set; }

        // Property to get the file icon path based on the file type
        public string FileIconPath
        {
            get
            {
                // If the file is an image, return the file path itself
                if (IsImage)
                    return FilePath;

                // Get the file extension in lowercase
                string extension = System.IO.Path.GetExtension(FilePath).ToLower();

                // Return the appropriate icon path based on the file extension
                switch (extension)
                {
                    case ".pdf":
                        return "Images/pdf_icon.png";
                    case ".doc":
                    case ".docx":
                        return "Images/doc_icon.png";
                    // Add more cases for other file types if needed
                    default:
                        return "Images/default_icon.png"; // Default icon for unknown file types
                }
            }
        }
    }
}
