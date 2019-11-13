﻿using System.IO;
using System.Threading.Tasks.Dataflow;
namespace cadnvert
{
    public class ValidateFile
    {
        public TransformBlock<string, WorkSet> Block { get; } = new TransformBlock<string, WorkSet>(file =>
        {
            if (!File.Exists(file))
                return new WorkSet() {FileIsValid = false};
            var templateFullPath =  TemplateMap.GetTemplate(Path.GetFileName(file));
            if(string.IsNullOrEmpty(templateFullPath))
                return new WorkSet() { FileIsValid = false };
            return new WorkSet()
            {
                FileIsValid = true, 
                File = file, 
                Template = templateFullPath
            };
        });
    }
}