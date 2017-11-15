# WebCrawler

This project is very like [SuperAgent](https://visionmedia.github.io/superagent/), just implemented in .Net Core platform.

## Get Started

```c#
    Response res = new Crawler()
        .Post("www.someurl.io/api/pet")
        .Field("name", "Manny")
        .Field("species", "cat")
        .SetHeader("X-API-Key", "foobar")
        .Type(Crawler.DataType.JSON)
        .End();    
```
## Request 

After your create a new or use exists Crawler object, the first method you should call is **Get** or **Post**.
When you call Get or Post method, the method will clear all data from previous request, except cookie and header. So if you call look like this 

```c#
    new Crawler().Field("name", "manny").Post("www.someurl.io/api/pet");
```
the field data will be clear.

Get and Post method accept absolute URLs only, because this project is not run in browser, it don't know your base URL.

**DELETE, HEAD, PATCH, PUT** have not been implemented yet. 

### Setting header fields

Setting header fields is very simple, invoke .SetHeader() with field name and value.

```c#
    new Crawler()
        .Get("www.some.io")
        .SetHeader("API-Key", "foobar")
        .End();
```
You may also pass an IDictionary or an Object to SetHeader() method. If you pass an IDictionary, method will get each KeyValuePair to header. If you pass an Object, method will get each fields in this object, use field's name is key and use field's value is value.

```c#
    private class Header
    {
        public string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng, application/json, text/javascript, */*;q=0.8";
        public string AcceptEncoding = "gzip, deflate, br";
        public string AcceptLanguage = "zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4";
        public string CacheControl = "max-age=0";
        public string Connection = "keep-alive";
        public string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
    }

    new Crawler()
        .Post("www.some.io")
        .SetHeader(new Header())
        .End();
```

### Get Request
The .Query() method accept name and value, which will be used in url's query-string. The follow will produce the path is www.some.io?query=Manny&range=1..5&order=desc

```c#
    new Crawler()
        .Get("www.some.io")
        .Query("query", "Manny")
        .Query("range", "1..5")
        .Query("order", "desc")
        .End();
```

### Post request
The .Query() method is the same role in Post request as it in Get request. 

The .Field() method accept name and value also, but which will be used in form data or json. 

The .Type() method decide Post request body is Crawler.DataType.JSON or Crawler.DataType.FORM. The follow request, when it is JSON, will produce a json object string which look like { query: 'Manny', range: '1..5', order: 'desc' }, when it is FORM, will produce a form string which look like query=Manny&range=1..5&order=desc.

```c#
    new Crawler()
        .Post("www.some.io")
        .Field("query", "Manny")
        .Field("range", "1..5")
        .Field("order", "desc")
        .End();
```
The .Attach() method accept a field name and a file path. If you use this method, the content-type will auto change to multipart/form-data. You can invoke this method many times, and request will post all files to server.

### Setting Cookies
The .SetCookie() accept key and value, and it will use your Get or Post target url's host as this cookie's domain. You can pass a string, which looks like "ptsd=somemeanlessguid;user=123;tls=nouse", it will be split by ";" and "=", then each of that will be passed to .SetCookie(key, value) also.

```c#
    new Crawler()
        .Post("www.some.io")
        .SetCookie("key", "value")
        .SetCookie("ptsd=somemeanlessguid;user=123;tls=nouse")
        .End();
```
## Response

Response can only get through Crawler.End() method. 

### Text
The .Text() method return the request raw text content. it will auto encoded by response's charactset.

### Charset
You can also use .Charset() method to appoint a charset to encoded the response's raw content.

### Body
If the response's raw data can be parsed to a json object, this .Body() method will return a NewtonJSON's JObject, otherwise it will return null.

### ToFile
Write all byte data from response to a file.





