


### ShouldBe Unit Test Library - Release Notes

*Requirements:*

* .NET Framework 3.0 or later (Tested with .NET 3.5 SP1)
* NUnit - Tested with NUnit v2.5.3.9345
* Rhino Mocks - Tested with Rhino Mocks 3.6.0.0

---
### v2.0.0 Release  22-AUG-2018
* Added VS2017 solution file.
* Using NuGet for NUnit reference.
* Removed Rhino Mocks support.

### v1.0.2 Release  09-AUG-2012
* Added missing API documentation
* Added Enumerable&lt;T&gt; assertions (unit tested)
 * ShouldBeAscending
 * ShouldBeAscending(keySelector)
 * ShouldBeDescending 
 * ShouldBeDescending(keySelector)

---
### v1.0.1 Release  07-MAR-2012

* Fixed enumeration tolerance assertions.
* Fixed bugs in failure messages and added more unit tests to check these.
* Arrays now display as { 1, 2, 3 } instead of [ 1, 2, 3 ]  to comply with C# language.

---
### v1.0.0 Release  06-MAR-2012

* Initial Release was adapted from Shouldly with 21 new methods added.

---
Edited with [Jon Com's Markup Editor](http://joncom.be/experiments/markdown-editor/edit/)