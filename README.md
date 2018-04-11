# NES-NN-Testbed [![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
#### Nintendo Entertainment System - Neural Network - Testbed


A C# Neural Network Testbed for Nintendo Entertainment System (NES) hardware based on the Xyene Emulator.NES emulator.


## Compilation
Emulator.NES uses C# 7 language features, so requires a compiler that supports them.

### Windows
Visual Studio 2017 is sufficient to compile.

### Linux
`msbuild` from Mono should be used to build, but the version included in most distro repositories is not
new enough to have C# 7 support (or may not have `msbuild`). Instead, [install a Mono version directly from the Mono site](http://www.mono-project.com/download/#download-lin).

Then, to compile:
```
$ nuget update -self
$ nuget restore
$ msbuild /property:Configuration=Release NES-NN-Testbed.sln
```
