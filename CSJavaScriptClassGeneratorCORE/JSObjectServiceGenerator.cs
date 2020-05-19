using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSJavaScriptClassGeneratorCORE
{
    class JSObjectServiceGenerator
    {

        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string ProjectName { get; set; }

        public Ac4yClass Ac4yClass { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "ObjectService";

        private const string ClassCodeMask = "#classCode#";
        private const string SuffixMask = "#suffix#";


        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = TemplatePath + TemplateSubPath + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".js", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetHead()
        {
            string propertyList = Ac4yClass.PropertyList.ToString();

            return ReadIntoString("Head")
                        .Replace(ClassCodeMask, Ac4yClass.Name)
                        .Replace(SuffixMask, Suffix)
                        ;

        }

        public string GetFoot()
        {
            return
                ReadIntoString("Foot")
                        .Replace(ClassCodeMask, Ac4yClass.Name)
                        .Replace(SuffixMask, Suffix)
                        ;

        }

        public string GetMethods()
        {
            return
                ReadIntoString("Methods")
                ;
        }

        public string GetRequestResponseClasses()
        {
            return
                ReadIntoString("RequestResponseClasses")
                        .Replace(ClassCodeMask, GetNameWithLowerFirstLetter(Ac4yClass.Name))
                ;
        }

        public JSObjectServiceGenerator Generate()
        {

            string result = null;

            result += GetHead();
            
            result += GetRequestResponseClasses();

            result += GetFoot();

            WriteOut(result, Ac4yClass.Name + Suffix, OutputPath);

            return this;

        } // Generate

        public JSObjectServiceGenerator Generate(Ac4yClass ac4yClass)
        {

            Ac4yClass = ac4yClass;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // JavaScriptClassGenerator