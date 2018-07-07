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

## Credits and Legalization

lrx is written by Alissa Sabre specifically based on the knowledges aquired through:
* XLIFF Version 1.2 specification (http://docs.oasis-open.org/xliff/v1.2/os/xliff-core.html)
* Text tool by swuforce, available on https://zenhax.com/viewtopic.php?t=1022 
* Unreal Engine 4 source code (Available for free of charge but not publicly.  See https://docs.unrealengine.com/en-us/GettingStarted/DownloadingUnrealEngine)

lrx depends on some third party software.  In particular, it depends on:
* [Mono.Options](https://github.com/xamarin/XamarinComponents/tree/master/XPlat/Mono.Options) nuget package.

Their mandate legalese follow:

### Xamarin Component for Mono.Options

**The MIT License (MIT)**

Copyright (c) .NET Foundation Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

20170421
