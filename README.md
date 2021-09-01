# About DotNetUrlPatternMatching

The library allows you to match a URL to a pattern.

How it works - we break the url pattern into parts  
And we match each non-empty part with a similar one from the URL.

You can specify Wildcard: `*` or `~`  
Where `*` is any character set within the group (schema, host, port, path, parameter, anchor)  
Where `~` any character set within a group segment (host, path)

Only supply parts of the URL you care about. Parts left out will match anything. E.g. if you don’t care about the host, then leave it out.


- [Quick Start](#quick-start)
    - [Installation](#installation)
	- [Simple Examples](#simple-examples)
- [URL parts](#url-parts)
    - [Scheme](#scheme)
    - [Host](#host)
    - [Port](#port)
    - [Path](#path)
    - [Query](#query)
    - [Fragment](#fragments)
    - [Basic Authentication](#basic-authentication)
- [Behavior](#behavior)
	- [Сombining](#сombining)
	- [Escaping](#escaping)
	- [Config](#config)
	- [Exceptions](#exceptions)

## Quick Start
nuget: https://www.nuget.org/packages/UrlPatternMatching/

* support all .NETStandard versions
* without dependencies

## Installation
* .NET CLI > dotnet add package UrlPatternMatching
* NPM > Install-Package UrlPatternMatching

## Simple Examples
```cs
using UrlPatternMatching;

string pattern = "http*://*.com/*/develop/README.md";
bool isMatch = "https://github.com/DotNetUrlPatternMatching/edit/develop/README.md".IsMatch(pattern);

// Should be - true
```
The highest performance will be achieved if you create an object UrlPatternMatcher for comparison.

```cs
using UrlPatternMatching;

var matcher = new UrlPatternMatcher("*:443/~/Dot~Matching");
bool isMatch = matcher.IsMatch(new Uri("https://github.com/org/DotNetUrlPatternMatching"));  

// Should be - true
```
To improve performance, you can use the `UrlPatternMatcher` caching

## URL parts
```
https://user:password@sub.domin.com:80/info/main/base?withParam=one#navigate
\___/   \___________/\_____________/\_/\____________/\____________/ \______/
  |           |             |        |      |             |           |
scheme    base-auth        host     port   path         query      fragment
```

All parts are optional. And if a part is not specified, then url can contain any value of a similar part.

## Scheme

Pattern | Matched | Not matched
--- |--- | ---
```https://github.com/``` | `https://github.com/` | `http://github.com/`
```http*://github.com/``` | `https://github.com/` | `ftp://github.com/`
```http://``` | `https://github.com/` | `ftp://github.com/`

## Host
`~` any character in domain level  
`*` any character in domain

Pattern | Matched | Not matched
--- |--- | ---
```github.com``` | `https://github.com/any` | `https://sub.github.com/`
```*.microsoft.com``` | `https://some.any.microsoft.com` | `https://microsoft.com`
```~soft.com``` | `https://microsoft.com` | `https://some.any.microsoft.com`
```*ozon.com``` | `https://mozon.co` | `https://mozon.comic.com`
```ya*.com``` | `https://ya.com` | `https://ya.co`
```ya~.com``` | `https://yaz.com` | `https://www.yaz.com`
```github*``` | `https://github.com` | `https://microsoft.com/github`
```192.168.1.~``` | `https://192.168.1.1/anyPath/` | `https://192.168.11.11/`
```192.*``` | `https://192.168.1.1/anyPath/` | `https://201.192.1.1`
```[ffff:~:~:ffff:*]``` | `[ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff]:83` | `[aaa:bbbb:ffff:ffff:ffff:ffff:ffff:ffff]`

## Port
Pattern | Matched | Not matched
--- |--- | ---
```http://github.com:80``` | `http://github.com` | `https://github.com`
```http://github.com:2*``` | `http://github.com:23` | `http://github.com:65`
```*:6564``` | `http://github.com:6564` | `http://github.com`

## Path
`~` any character in path  
`*` any character in segment of path

Pattern | Matched | Not matched
--- |--- | ---
```/api/user/get``` | `https://github.com/api/user/get?w=1` | `https://github.com/api/user/get/45/`
```/api/us~``` | `https://github.com/api/users` | `https://github.com/api/user/get`
```/api/us*``` | `https://github.com/api/user/get` | `https://github.com/svc/api/user`
```/api/user/~/get``` | `https://github.com/api/user/8787/get` | `https://github.com/api/user`
```github.com/*api/users``` | `https://github.com/v3/api/users` | `https://github.com/v3/api/users/get`

## Query

To match parameters in the template, the parameter (or part of it), the `=` sign, as well as the value (or part of it) must be specified

For case sensitive comparison, you can set the parameters: `IsCaseSensitiveParamNames` or `IsCaseSensitiveParamValues` in config

Pattern | Matched | Not matched
--- |--- | ---
```?cc=33&aa=1*``` | `http://github.com?aa=11&bb=22&cc=33` | `http://github.com`
```?cc=33&a*=11``` | `http://github.com?abs=11&bb=22&cc=33` | `http://github.com?cc=33&bba=11`
```http://github.com??text=%D0*``` | `http://github.com??text=%D0%BC%D0%BE%D0%BB` | `http://github.com??text=%BC%D0`

## Fragments
Pattern | Matched | Not matched
--- |--- | ---
```http://github.com#main*``` | `http://github.com#maintable` | `https://github.com#table`
```http://github.com#main*page*load``` | `http://github.com#mainAnyPageWillLoad` | `http://github.com#baseMainAnyPageWillLoad`
```http://github.com#*load``` | `http://github.com#mainPageLoad` | `http://github.com#mainPageLoadThen`
```#main``` | `http://github.com#main` | `https://main.com`

## Basic Authentication
We can check basic authentication in url (support not all browsers)

Pattern | Matched | Not matched
--- |--- | ---
```https://myUser:MyPassword@github.com``` | `https://myUser:MyPassword@github.com` | `https://github.com`
```https://myUser:@github.com``` | `https://myUser:MyPassword@github.com` | `https://other:any@github.com`
```https://mail*:@github.com``` | `https://mail1:pass@github.com` | `https://other:mail@github.com`

## Behavior
Schema and host are always case insensitive. 

## Сombining
You can combine different parts in the template and specify several wildcards

Example: `*nuget*/~/~/?top=*` should be matched with `https://www.nuget.org/packages/UrlPatternMatching?top=100`

Also you can skip any parts and specify, for example, in the pattern only the scheme and the fragment

Example: `https://#page`

## Escaping
Matching can be given escaped characters (`UrlDecode` \ `UrlEncode`)

Pattern | will matched
--- |---
```#молоко``` | `https://github.com#%D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE`
```github.com#молоко``` | `https://github.com#молоко`
```#%D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE``` | `https://github.com#молоко`
```#%D0*``` | `https://github.com#D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE`

## Config
For gloabal settings use `Config.Default`, or create a `new Config()`.

Config contains case sensitivity settings for most parts. For others will be ignore case sensitive.
```cs
	bool IsCaseSensitivePathMatch { get; set; } = false;
	bool IsCaseSensitiveFragmentMatch { get; set; } = false;
	bool IsCaseSensitiveParamNames { get; set; } = false;
	bool IsCaseSensitiveParamValues { get; set; } = false;
	bool IsCaseSensitiveUserAndPassword { get; set; } = true;
```
Example:

```cs
var config = new Config { IsCaseSensitivePathMatch = true };
var matcher = new UrlPatternMatcher("/atlassian.net/jira/your-work/", config);
bool result = matcher.IsMatch("https://any.atlassian.net/jira/Your-Work");
```
If the config is not specified, then the default config will be applied, it does not need to be explicitly changed in the parameters.

Example:

```cs
Config.Default.IsCaseSensitiveParamValues = true; 
```
Default config can be passed as parameters for `UrlExtensions.IsMatch  `

Example:

```cs
var config = new Config();
bool isMatch = "https://github.com".IsMatch("*.com", config);
```

## Exceptions

The library may throw exceptions of type `InvalidPatternException` or `UriFormatException`
