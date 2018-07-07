# lrx
A tool to convert UE .locres to XLIFF for translation and convert the signed-off file back.

## Description

lrx is a small tool to perform the following three tasks:
* Reading contents of a .locres file and producing an XLIFF file for it with empty target texts.
* Reading contents of two .locres files, aligning texts in them and producing an XLIFF file with aligned source and target texts.
* Reading a translated XLIFF file and creating a new .locres file containing the target texts.

Note that lrx can only read an XLIFF that is originally produced by lrx.

## XLIFF

XLIFF is an XML based file format standardized by OASIS (https://www.oasis-open.org/committees/tc_home.php?wg_abbrev=xliff) and is widely accepted by software localization industry.
XLIFF is supported by most (if not all) of modern CAT (Computer Aided Translation) tools.

## Environment

* lrx runs on .NET framework 4.5 or compatible CLR, including Mono.  It does nothing specific to Windows, so it should run on Mac or Linux, too.
* The core part of lrx is a command line tool.  It also has a GUI (window) front-end, lrxw, that runs on WinForms.
* I'm using Visual Studio 2017 Express.

## Status

The basic functions of lrx and lrxw are now working.
