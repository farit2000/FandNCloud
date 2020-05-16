using System.Collections.Generic;
using FandNCloud.Common.Responds.HelperModels;

namespace FandNCloud.Common.Responds
{
    public class BrowseFolderRespond : IRespond
    {
        public List<FolderInfo> Folders { get; set; }
        public List<FileInfo> Files { get; set; }

        public BrowseFolderRespond(List<FolderInfo> folders, List<FileInfo> files)
        {
            Files = files;
            Folders = folders;
        }
    }
}