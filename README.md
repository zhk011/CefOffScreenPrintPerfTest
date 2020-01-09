# CefOffScreenPrintPerfTest
This demo project shows there's an performance degradation on the browser.PrintToPdfAsync function when upgrading Cefsharp OffScreen from V57 to V75.

It uses browser.PrintToPdfAsync to print a web page that is around 3MB to pdf file. With CefSharp 57.0.0, the average time is around 280ms for PrintToPdfAsync to complete; 
When upgraded to V75.1.143, the average time jumps to 700ms.

Steps to repro:

1. Download and run the project (default with CefSharp V57), wait for the project to complete and show the printed PDF, Find the last few lines of console window:
    Screenshot saved.  Launching your default image viewer... takes XXX MS

2. Upgrade the CefSharp references to latest stable V75, then run the project again, compare the time taken.

