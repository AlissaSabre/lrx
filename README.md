# lrx
A tool to convert UE .locres to XLIFF for translation and convert the deliverable back.

## Description

lrx is a small tool to perform the following three tasks:
* Reading contents of a .locres file and producing an XLIFF file with empty target texts.
* Reading contents of two .locres files, matching texts in them and producing an XLIFF file with matched source and target texts.
* Reading a translated XLIFF file and creating a new .locres file containing the target texts.

Note that lrx can only read an XLIFF that has an identical structure to the lrx's XLIFF.

## Environment

* lrx runs on .NET framework 4.5 or compatible CLR, including Mono.
* The core part of lrx is a command line tool.  It also has a GUI (window) front-end, that runs on WinForms.
* I'm using Visual Studio 2017 Express.

## Status

lrx is a brand-new project.  It doesn't work yet.
