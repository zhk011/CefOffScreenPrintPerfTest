using CefSharp;
using CefSharp.OffScreen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CefPerfTest
{
    class Program
    {
        private static ChromiumWebBrowser browser;

        public static void Main(string[] args)
        {
            const string testUrl = "https://forms.office.com/Pages/PrintTemplatePage.aspx";

            Console.WriteLine("This example application will load {0}, take a screenshot, and save it to your desktop.", testUrl);
            Console.WriteLine("You may see Chromium debugging output, please wait...");
            Console.WriteLine();

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };

            settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            // Create the offscreen Chromium browser.
            browser = new ChromiumWebBrowser(testUrl);

            // An event that is fired when the first page is finished loading.
            // This returns to us from another thread.
            browser.LoadingStateChanged += BrowserLoadingStateChanged;

            // We have to wait for something, otherwise the process will exit too soon.
            Console.ReadKey();

            // Clean up Chromium objects.  You need to call this in your application otherwise
            // you will get a crash when closing.
            Cef.Shutdown();
        }

        private static readonly Size A3PageSize = new Size(297000, 420000);

        private static void BrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // Check to see if loading is complete - this event is called twice, one when loading starts
            // second time when it's finished
            // (rather than an iframe within the main frame).
            if (!e.IsLoading)
            {
                // Remove the load event handler, because we only want one snapshot of the initial page.
                browser.LoadingStateChanged -= BrowserLoadingStateChanged;

                var item ="{ \"id\":\"v4j5cvGGr0GRqy180BHbR3Yzu5M5L4VKtiQzSoz6zTxUREVYM1kyUUdDUFFKWVUzT1oxSjhZTExGUi4u\",\"title\":\"Course Evaluation Survey\",\"modifiedDate\":\"2020-01-06T08:49:10.3915722Z\",\"createdDate\":\"2019-12-11T03:42:47.79Z\",\"meetingId\":null,\"ownerId\":\"93bb3376-2f39-4a85-b624-334a8cfacd3c\",\"ownerTenantId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"description\":\"Improve curriculum and teaching methods by hearing directly from students.\",\"questions\":[{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"rc9e7b087e0144b40a41479406f531da9\",\"isQuiz\":false,\"order\":700.0,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"Choices\\\":[],\\\"Length\\\":10,\\\"RatingShape\\\":\\\"Number\\\",\\\"LeftDescription\\\":\\\"Not at all likely\\\",\\\"RightDescription\\\":\\\"Extremely likely\\\",\\\"ShowRatingLabel\\\":true,\\\"MinRating\\\":1,\\\"MathPatterns\\\":[],\\\"MathAriaLabels\\\":[]}\",\"required\":false,\"title\":\"How likely are you to recommend this course to a friend or classmate?\",\"type\":\"Question.Rating\",\"allowCustomChoice\":null,\"trackingId\":\"06573e669c844585b149d3534affd76f\"},{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2020-01-06T03:29:57.3031778Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":\"Unknown\",\"isFromSuggestion\":false,\"id\":\"r16ac33ee7747434e9bc12f46f790fcb2\",\"isQuiz\":false,\"order\":625.0,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Option 1\\\",\\\"IsGenerated\\\":true},{\\\"Description\\\":\\\"Option 2\\\",\\\"IsGenerated\\\":true}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"how do you rate it?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"15319689f4f0454baba05f3adb87c347\"},{\"groupId\":null,\"defaultValue\":\"null\",\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"ra0a59ffc1fff48a9a5169129b8300732\",\"isQuiz\":false,\"order\":687.5,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Extremely well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Very well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Somewhat well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not so well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not at all well\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\",\\\"MathPatterns\\\":[],\\\"MathAriaLabels\\\":[]}\",\"required\":false,\"title\":\"How well did the instructor communicate course expectations?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"e4b1029e65834958988bdf2b9ae513d8\"},{\"groupId\":null,\"defaultValue\":\"null\",\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"r65dae93ef014498387e654f6e6d0a4ba\",\"isQuiz\":false,\"order\":600.0,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Very satisfied\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Satisfied\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Neither satisfied nor dissatisfied\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Dissatisfied\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Very dissatisfied\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Option 6\\\",\\\"IsGenerated\\\":true}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"How satisfied are you with the knowledge you gained throughout the course?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"c3bc544bc1f74c3082b955a3530a09a9\"},{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"r491d56e985ef4692a196e50b5e8dd07e\",\"isQuiz\":false,\"order\":675.0,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"Length\\\":10,\\\"RatingShape\\\":\\\"Number\\\",\\\"LeftDescription\\\":\\\"Poor\\\",\\\"RightDescription\\\":\\\"Excellent\\\",\\\"ShowRatingLabel\\\":true,\\\"MinRating\\\":1}\",\"required\":false,\"title\":\"How would you rate the instructor's overall teaching performance?\",\"type\":\"Question.Rating\",\"allowCustomChoice\":null,\"trackingId\":\"44ff3eff1a5c4080b0243d4fbb034eed\"},{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"r9f04d901963f476390b0053a9fad5536\",\"isQuiz\":false,\"order\":698.4375,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Extremely effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Very effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Somewhat effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not so effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not at all effective\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"How effective were the instructional materials used in the course?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"5a15b325c321490a9a77aa7ec12b8111\"},{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"rd43902d83dcb42d5a076e6d95edc9193\",\"isQuiz\":false,\"order\":693.75,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Extremely well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Very well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Somewhat well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not so well\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not at all well\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"How well did your instructor communicate course assignments?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"efc47aa7988a4eff83d1e03981ac30d2\"},{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"rac998bf9695f4e4fb1ac170c12e7869d\",\"isQuiz\":false,\"order\":699.21875,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Extremely effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Very effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Somewhat effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not so effective\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not at all effective\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"How effective were the learning activities used in this course?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"f6c538816cf8470b9ea28c351cce8be6\"},{\"groupId\":null,\"defaultValue\":\"null\",\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"r70df4624aab8415dbc1ddd8e2809fc9b\",\"isQuiz\":false,\"order\":699.609375,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Yes\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"No\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not sure\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"Did the course meet your expectation?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"00420d4c53df4dbc9eb55ac434aadeee\"},{\"groupId\":null,\"defaultValue\":\"null\",\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"rdafeb0db8903448f9d0cc41541510aaf\",\"isQuiz\":false,\"order\":650.0,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Yes\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"No\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not sure\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"Do you feel you achieved your desired learning outcome?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"19341ce17f3145c48f12e96f8105083b\"},{\"groupId\":null,\"defaultValue\":null,\"image\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"modifiedDate\":\"2019-12-11T03:42:47.2593388Z\",\"status\":\"Added\",\"subtitle\":null,\"allowMultipleValues\":null,\"choices\":[],\"titleHasPhishingKeywords\":false,\"subtitleHasPhishingKeywords\":false,\"fileUploadSPOInfo\":null,\"formsProRTQuestionTitle\":null,\"formsProRTSubtitle\":null,\"questionTagForIntelligence\":null,\"isFromSuggestion\":false,\"id\":\"r31805d3d6ddf4d459248d68ddbb467b6\",\"isQuiz\":false,\"order\":696.875,\"deserializedQuestionInfo\":null,\"questionInfo\":\"{\\\"ChoiceType\\\":1,\\\"Choices\\\":[{\\\"Description\\\":\\\"Extremely prepared\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Very prepared\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Somewhat prepared\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not so prepared\\\",\\\"IsMathOption\\\":false},{\\\"Description\\\":\\\"Not at all prepared\\\",\\\"IsMathOption\\\":false}],\\\"OptionDisplayStyle\\\":\\\"ListAll\\\"}\",\"required\":false,\"title\":\"How prepared was your instructor at the start of each class?\",\"type\":\"Question.Choice\",\"allowCustomChoice\":null,\"trackingId\":\"0367241a1cb94eb1b21f7360925a947c\"}],\"descriptiveQuestions\":[],\"permissions\":[{\"permissionSet\":\"Read, Write, Share\",\"principalId\":\"93bb3376-2f39-4a85-b624-334a8cfacd3c\",\"formId\":\"v4j5cvGGr0GRqy180BHbR3Yzu5M5L4VKtiQzSoz6zTxUREVYM1kyUUdDUFFKWVUzT1oxSjhZTExGUi4u\",\"type\":\"User\",\"principalName\":null,\"principalEmailAddress\":null,\"isOwner\":true}],\"permissionTokens\":[],\"settings\":\"{\\\"IsAnonymous\\\":true,\\\"IsQuizMode\\\":false}\",\"background\":{\"altText\":\"\",\"contentType\":\"image/jpeg\",\"fileIdentifier\":\"0923279d-1fb0-4221-ad62-9f6560bdc84f\",\"originalFileName\":\"e3d0efb5-acae-47e9-8cba-401ad1a8d554\",\"resourceId\":\"8ab57ee5-8ce8-480b-9565-4afce3645373\",\"resourceUrl\":\"https://lists.office.com/Images/72f988bf-86f1-41af-91ab-2d7cd011db47/93bb3376-2f39-4a85-b624-334a8cfacd3c/TDEX3Y2QGCPQJYU3OZ1J8YLLFR/8ab57ee5-8ce8-480b-9565-4afce3645373\",\"height\":null,\"width\":null,\"size\":null},\"otherInfo\":\"{\\\"PrimaryCustomizedThemeColor\\\":\\\"#012A52\\\",\\\"SecondaryCustomizedThemeColor\\\":\\\"#8F327E\\\",\\\"TertiaryCustomizedThemeColor\\\":\\\"#F2E6F0\\\",\\\"Theme\\\":{\\\"Name\\\":\\\"Suggest_Competition_Athlete\\\"},\\\"TemplateId\\\":\\\"CourseEvaluation\\\"}\",\"logo\":{\"altText\":null,\"contentType\":null,\"fileIdentifier\":null,\"originalFileName\":null,\"resourceId\":null,\"resourceUrl\":null,\"height\":null,\"width\":null,\"size\":null},\"xlFileUnSynced\":false,\"category\":null,\"trackingId\":\"2DA0CEEF-818D-4D98-A572-D4CC4F58B8EE\",\"predefinedResponses\":null,\"thankYouMessage\":null,\"emailReceiptEnabled\":null,\"flags\":4,\"onlineSafetyLevel\":0,\"type\":\"form\",\"defaultLanguage\":null,\"localeList\":[]}";
                var javaScriptString = string.Format("Forms.PrintTemplate.initializePage({0}, {1}, {2}, {3});", item, "null", "null", "false");

                var scriptTask = browser.EvaluateScriptAsync(javaScriptString);

                scriptTask.ContinueWith(t =>
                {
                    //Give the browser a little time to render
                    Thread.Sleep(1000);
                    // Wait for the screenshot to be taken.

                    var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CefSharp screenshot.pdf");
                    Stopwatch watch = new Stopwatch();
                    Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);

                    var task= browser.PrintToPdfAsync(screenshotPath,
                        new PdfPrintSettings
                        {
                            BackgroundsEnabled = true,
                            HeaderFooterEnabled = false,
                            PageWidth = A3PageSize.Width,
                            PageHeight = A3PageSize.Height,
                            MarginType = CefPdfPrintMarginType.None,
                        });
                    watch.Start();
                    task.ContinueWith(x =>
                    {
                        // The time with V57 is around 250ms, with V75 average increases to 700ms.
                        watch.Stop();
                        Console.WriteLine();

                        Console.WriteLine($"Screenshot saved.  Launching your default image viewer... takes {watch.ElapsedMilliseconds} MS");

                        // Tell Windows to launch the saved image.
                        Process.Start(new ProcessStartInfo(screenshotPath)
                        {
                            // UseShellExecute is false by default on .NET Core.
                            UseShellExecute = true
                        });

                        Console.WriteLine("Image viewer launched.  Press any key to exit.");
                    }, TaskScheduler.Default);
                });
            }
        }
    }
}
