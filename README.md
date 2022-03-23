# http_testing

Just how I like to test HTTP

I have an example, which is the open api specification for health checks. Open api documents are found at in the ```/public``` folder in
https://github.com/bjartwolf/documentation-repo-example
Check out that repo if you are interested in how to write contracts by ✍, btw.

## The client
The client will use the ```openapi.json``` specification and build a .NET Client.
I generate the client because I am lazy. For instructions, [see](https://kaylumah.nl/2021/05/23/generate-csharp-client-for-openapi.html)

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