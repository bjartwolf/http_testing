# http_testing

**tl;dr; Mock the factory that creates HttpClients, use real HttpClients and real deserializers.** 
**For added bonus points, we can use examples from the Open API specification in the tests.**

This README file describes how I like to test HTTP in .NET. Since the introduction of [IHttpClientFactory](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0)
things have become a lot easier. The factory handles a lot of hard stuff that deals with TCP/IP and port reuse and whatnot. 
I think, since this is "new", I sometimes still see the interfaces being mocked being at some sort of service layer above HTTP. It quickly becomes weird.

I have based the code on a simple example, which is the open api specification for health checks. Open api documents are found at in the ```/public``` folder in
https://github.com/bjartwolf/documentation-repo-example
Check out that repo if you are interested in how to write contracts by ✍, btw. Also, that repo can be started to look at the API contract in redoc.

## The client
The client will use the ```openapi.json``` specification and build a .NET Client automatically. I mostly did this to demonstrate that I do not always argue to do everything by hand, and for the sake
of this blogpost, how the client is generated is not really that important.
It should automatically build, and it is based on instructions I found 
[here.](https://kaylumah.nl/2021/05/23/generate-csharp-client-for-openapi.html)

This client accepts a HttpClient to use for its requests.

## The server
The server has registered the IHttpClientFactory using
```csharp
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient();
```

## The tests

The tests can create its own IHttpClientFactories, that create real HttpClients that return the examples from the documentation-repo-example mentioned above.
Since the contracts are written in such a way that the examples are seperate, embedded files, it is easy for the fakes to just return them as content.
There are no mocks, and no fake HttpClients. Everythin except the factory and the HttpClientHandlers are what you expect...


# The strawman
I lied. I implemented the strawman too. You can find it in the strawman folder. Notice how little of the client is actually implemented. 
I tried to do both in a TDD kind of way, and it turned out I did not have to implement anything to make the test pass, because the interface is almost doing nothing.