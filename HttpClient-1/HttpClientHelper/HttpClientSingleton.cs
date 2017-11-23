using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpClient_1.HttpClientHelper
{

    //public sealed class HttpClientSingleton
    //{
    //    // A private constructor to restrict the object creation from outside
    //    private HttpClientSingleton()
    //    {
    //    }
    //    // A private static instance of the same class
    //    private static readonly HttpClientSingleton instance = null;
    //    static HttpClientSingleton()
    //    {
    //        // create the instance only if the instance is null
    //        instance = new HttpClientSingleton();
    //    }
    //    public static HttpClientSingleton GetInstance()
    //    {
    //        // return the already existing instance
    //        return instance;
    //    }
    //}
    /// <summary>
    ///  Instantiation is triggered by the first reference to the static member of the nested class, 
    ///  which only occurs in Instance. This means the implementation is fully lazy, 
    ///  but has all the performance benefits of the previous versions of singleton pattern. 
    ///  Note that although nested classes have access to the enclosing class's private members, the reverse is not true, 
    ///  hence the need for instance to be internal here. That doesn't raise any other problems, though, as the class itself is private. 
    ///  The code is a bit more complicated in order to make the instantiation lazy, however.
    /// </summary>
    public sealed class HttpClientSingleton
    {
        private static readonly Lazy<HttpClientSingleton> lazy =
            new Lazy<HttpClientSingleton>(() => new HttpClientSingleton());

        public static HttpClientSingleton Instance { get { return lazy.Value; } }

        private HttpClientSingleton()
        {
        }
    }
}