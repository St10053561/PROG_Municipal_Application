namespace MunicipalService.Classes
{
    public class FileItem
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsImage { get; set; }
        public string FileIconPath
        {
            get
            {
                if (IsImage)
                    return FilePath;

                string extension = System.IO.Path.GetExtension(FilePath).ToLower();
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
