using Ivy.IoC;
using Ivy.Pdf.Templating.Core.Interfaces.Services;
using Ivy.Pdf.Templating.IoC;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Ivy.Pdf.Templating.Test.Services
{
    public class TemplateGenerationTest
    {
        #region Variables & Constants

        private readonly IPdfTemplatingService _sut;

        private const string templatePath = "../../../Artifacts/IAGE_Template.pdf";
        private const string resultPath = "../../../Results/result_template.pdf";

        #endregion

        #region SetUp & TearDown

        public TemplateGenerationTest()
        {
            var containerGen = new ContainerGenerator();

            containerGen.InstallIvyPdfTemplating();

            _sut = containerGen.GenerateContainer().GetService<IPdfTemplatingService>();
        }

        #endregion

        #region Tests

        //[Fact(Skip = "Console Test")]
        [Fact]
        public void TestGeneratePdfFromTemplate()
        {
            if (File.Exists(resultPath))
            {
                File.Delete(resultPath);
            }

            var todayDate = DateTime.Now.Date.ToString("MMMM dd, yyyy");

            using (var templateStream = new FileStream(templatePath, FileMode.Open))
            {
                using (var resultStream = new FileStream(resultPath, FileMode.Create))
                {
                    var dict = new Dictionary<string, string>
                    {
                        { "StudentName", "Anne Marie Woolsey" },
                        { "ClassName", "Test Class Sample Name" },
                        { "CompletionDate", todayDate }
                    };

                    _sut.TemplatePdf(templateStream, dict, resultStream);
                }
            }
        }

        #endregion
    }
}
