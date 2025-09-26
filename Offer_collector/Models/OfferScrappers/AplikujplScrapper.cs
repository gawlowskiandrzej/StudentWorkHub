using HtmlAgilityPack;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.OfferScrappers
{
    internal class AplikujplScrapper : BaseHtmlScraper
    {
        string _offerListHtml;
        public override async Task<(string, string)> GetOfferAsync(string url = "")
        {
            _offerListHtml = await GetHtmlSource(url);
            await GetOfferList();


            return (null, null);
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        async Task<List<AplikujplSchema>> GetOfferList()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(_offerListHtml);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-list\"]");
            List<OfferListHeader> offerListHeader = new List<OfferListHeader>();
            List<AplikujplSchema> offerList = new List<AplikujplSchema>();

           // foreach (HtmlNode offerNode in node.SelectNodes(".//li[contains(concat(' ', normalize-space(@class), ' '), ' offer-card ')]"))
            //{
                //AplikujplSchema ap = new AplikujplSchema();
                //OfferListHeader header = GetHeader(offerNode);
                //if (header != null)
                //{
                    //ap.header = header;

                    //string detailsUrl = AplikujPlUrlBuilder.baseUrl + header.link;
                    /*ap.details =*/ GetOfferDetails(/*await GetHtmlSource(detailsUrl)*/null);

                    //offerList.Add(ap);
                //}
            //}

            return offerList;
        }
        OfferListHeader GetHeader(HtmlNode node)
        {
            var header = new OfferListHeader();

            //TODO fix nulls

            header.title = node.SelectSingleNode(".//a[@class='offer-title']")?.InnerText.Trim() ?? "";

            header.link = node.SelectSingleNode(".//a[@class='offer-title']")?.GetAttributeValue("href", "") ?? "";

            header.company = node.SelectSingleNode(".//div[@class='text-sm']/a")?.InnerText.Trim() ?? "";

            header.location = node.SelectSingleNode(".//li[contains(@class,'workPlace')]/span")?.InnerText.Trim() ?? "";

            header.employmentType = node.SelectSingleNode(".//li[contains(@class,'employmentType')]/span")?.InnerText.Trim() ?? "";

            header.companyLogoUrl = node.SelectSingleNode(".//div[@class='offer-card-thumb']//img")?.GetAttributeValue("src", "");

            header.dateAdded = node.SelectSingleNode(".//time")?.InnerText.Trim() ?? "";

            header.recomended = node.SelectSingleNode(".//span[contains(@class,'offer-badge')]") != null;

            header.salary = node.SelectSingleNode(".//span[contains(@class,'offer-salary')]")?.InnerText.Trim();
            header.remoteOption = node.SelectSingleNode(".//span[contains(@class,'offer-card-labels-list-item--remoteWork')]") != null;

            return header;
        }
        AplikujPl.Dates GetDates(HtmlNode node)
        {
            AplikujPl.Dates date = new AplikujPl.Dates();
            HtmlNodeCollection dateCollection = node.SelectNodes(".//div[contains(@class,'offer__dates')]//time");

            if (dateCollection.First() != null)
            {
                HtmlNode publishNode = dateCollection.First();
                string publishRaw = publishNode.GetAttributeValue("datetime", null);
                if (DateTime.TryParse(publishRaw, out var publishDate))
                    date.publishionDate = publishDate;
            }

            if (dateCollection.ElementAt(1) != null)
            {
                HtmlNode expireDate = dateCollection.ElementAt(1);
                string expireRaw = expireDate.GetAttributeValue("datetime", null);
                if (DateTime.TryParse(expireRaw, out var expirationDate))
                    date.expirationDate = expirationDate;
            }

            return date;
        }
        AplikujPl.Company GetCompany(HtmlNode topDeatailHeader)
        {
            HtmlNode companyNode = topDeatailHeader.SelectSingleNode(".//div[contains(@class,'text-md lg:text-base')]//a");
            AplikujPl.Company company = new AplikujPl.Company();
            company.company = companyNode.InnerText.Trim();
            company.companyLink = companyNode.GetAttributeValue("href", "") ?? "";
            company.companyLogo = topDeatailHeader.SelectSingleNode(".//div[contains(@class,'mr-2 mt-2 sm:mr-8 sm:mt-0')]//img")?.GetAttributeValue("src", "") ?? "";

            return company;
        }
        OfferDetails GetOfferDetails(string html)
        {
            html = @"

<!DOCTYPE html>
<html lang=""pl-PL"">
<head>
    <meta charset=""utf-8"">
        <title>Oferta pracy Magazynier // 5.570 zł // od zaraz, Gi Group S.A. - Sosnowiec - Aplikuj.pl</title>
        <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
        <meta name=""description"" content=""Zapoznaj się z ofertą pracy Magazynier // 5.570 zł // od zaraz w Sosnowcu. Pracownika poszukuje firma Gi Group S.A.. Aplikuj już dzisiaj!"">
    
            <link rel=""canonical"" href=""https://www.aplikuj.pl/oferta/2960357/magazynier-5-570-zl-od-zaraz-umowa-o-prace-gi-group-s-a-"">
    
                         <meta name=""robots"" content=""index,follow"">
          
    <meta name=""format-detection"" content=""telephone=yes"">
    <meta name=""apple-itunes-app"" content=""app-id=1477877286"">
    <meta name=""google-play-app"" content=""app-id=pl.aplikuj.mobile"">
    <meta name=""HandheldFriendly"" content=""true"">
    <meta name=""apple-mobile-web-app-title"" content=""Aplikuj.pl - oferty pracy"">
    <meta name=""apple-mobile-web-app-capable"" content=""yes"">
    <meta name=""mobile-web-app-capable"" content=""yes"">
    <meta name=""application-name"" content=""Aplikuj.pl - oferty pracy"">
    <meta name=""google-site-verification"" content=""PypLOWuPHYA2ZMCy7h01nBGLHMK3Y3bEVELSNkLgwaY"">
    <link href=""/build/favicons/favicon-16x16.532d85ae.ico"" rel=""shortcut icon"" type=""image/x-icon"">
    <link href=""/build/favicons/favicon-192x192.5a07269b.png"" rel=""icon"" type=""image/png"" sizes=""192x192"">
    <link href=""/build/favicons/favicon-192x192.5a07269b.png"" rel=""apple-touch-icon"" type=""image/png""
          sizes=""192x192"">
    <meta name=""msapplication-TileImage"" content=""/build/favicons/favicon-192x192.5a07269b.png"">
    <link rel=""manifest"" href=""/build/manifest.json"">
        <link rel=""preconnect"" href=""https://www.googletagmanager.com"">
    <link rel=""preconnect"" href=""https://www.artfut.com"">
        <link rel=""preload"" href=""/build/fonts/Inter-roman.var.ba4caefc.woff2"" as=""font"" type=""font/woff2"" crossorigin>
    <style> @font-face {
            font-family: ""Inter var"";
            font-weight: 100 900;
            font-display: swap;
            font-style: normal;
            font-named-instance: ""Regular"";
            src: url(""/build/fonts/Inter-roman.var.ba4caefc.woff2"") format(""woff2"");
        }
    </style>

        <meta property=""og:title"" content=""    Oferta pracy Magazynier // 5.570 zł // od zaraz, Gi Group S.A. - Sosnowiec
"">
    <meta property=""og:description"" content=""                
    Zapoznaj się z ofertą pracy Magazynier // 5.570 zł // od zaraz w Sosnowcu. Pracownika poszukuje firma Gi Group S.A.. Aplikuj już dzisiaj!
"">
    <meta property=""og:url"" content=""https://www.aplikuj.pl/oferta/2960357/magazynier-5-570-zl-od-zaraz-umowa-o-prace-gi-group-s-a-"">
    <meta property=""og:type"" content=""website"">
            <meta property=""og:image"" content=""/media/og_image/6-2mJYa-_fQUmzbKho1Z7o2iuD0IoPmWSVxoPgtQiPwJumDDws_o3Pu5Y51EQm1sQ712X9LNSGQSoEzGi2I_IgF7Dn8sGUGX2HigAXPKsuDDTAaMS11A4AcXEGRxdXNEvSEtuCmVeFOFVixy2ElJ9U0z8Z0HrM7S9Kdr1yxtJgbZRWdL_2KedxrIlxz3gmVwRxOgWXTZrGHi2Ijc7cg1xOZ6gzdmB8z4t07KSXAEGRzgn3x_w1dWpBQIIv3j_vzJlCJD-VLQyCcliFtu0Ybsb0jG6lQj8cjJ7jujRmBg28xSQoWaHUqay5OFxdWANvEiaO-EWfA-lU4k7FJJ77ZG0sf9wDmFUXvNCJmqeobo8_AVrJAqk8jRGuU417WHoMIxwx25kcE8eleZCBXNJkgAwcSuhHqm1nBOSAlJIymbJIQj9D9r3_tNLnbk6B5fChOw-c9ThivwqReIPm03bps/2960357.jpg"">
        <meta property=""og:image:width"" content=""800"">
        <meta property=""og:image:height"" content=""533"">
        <meta property=""og:site_name"" content=""Aplikuj.pl"">
    <meta property=""og:locale"" content=""pl_PL"">
    <meta property=""og:ttl"" content=""1209600"">

            <script src=""/build/runtime.4d23dce5.js"" defer></script><script src=""/build/75.5493fa3c.js"" defer></script><script src=""/build/js/main.60fbdf4c.js"" defer></script>
    
        <script>
        window.lazyScriptsToLoad = window.lazyScriptsToLoad || [];
        window.lazyScriptsToLoad.push({
            id: 'admitad',
            url: 'https://www.artfut.com/static/tagtag.min.js?campaign_code=751029cb10',
            admitadFallback: true
          });
                    </script>
    
    
            <link rel=""stylesheet"" href=""/build/css/main.8a620cd0.css"">
    </head>
<body>
    <script>
      window.dataLayer = window.dataLayer || [];
          </script>
    <script defer>
      (function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
          new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
        j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
        'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
      })(window,document,'script','dataLayer','GTM-WVK5S78');
    </script>

    <script> const jsConfig = { env: 'prod', domain: 'aplikuj.pl', isMobile: false, apiUrl: '/ajax', searchFormInitData: { salary: 0, period: '', } };window.jsConfig = jsConfig;</script>

    <header
        id=""page-header""
        x-data=""pageHeader""
        x-breakpoint=""setHeaderHeight""
        class=""print:hidden""
    >
            
    
    
    
    
    
    
    
    <nav class=""navbar"" x-data=""{'mobileMenu':false}"">
        <div class=""mx-auto px-4 lg:px-6"">
            <div class=""main-menu"">
                <div class=""flex"">
                    <div class=""flex-shrink-0 flex items-center"">
                        
                        <a href=""https://www.aplikuj.pl"" title=""Strona główna"">
                            <img class=""block h-[36px] w-auto"" width=""133"" height=""36"" src=""/build/logo/logo-blue.80d8c0e2.svg"" alt=""Aplikuj.pl"">
                        </a>
                    </div>
                    <div class=""hidden lg:ml-6 xl:flex lg:space-x-1 2xl:space-x-4"">
                                <a href=""/praca""
           class=""nav-link active"" title="""">
            Oferty pracy
        </a>
        <a href=""/pracodawcy"" class=""nav-link"" title="""">
            Pracodawcy
        </a>
        <a href=""/porady"" class=""nav-link"" title="""">
            Porady
        </a>
        <a href=""/cv/"" class=""nav-link"" title="""">
            Kreator CV
        </a>
        <a href=""/kalkulator-wynagrodzen"" class=""nav-link"" title="""">
            Kalkulator wynagrodzeń
        </a>
                    <a href=""/dla-pracodawcow"" class=""nav-link special"" title="""">
                        
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512"" class=""svg-inline--fa fa-angle-down text-secondary fa-1x font-bold mr-1"" style=""""><path fill=""currentColor"" d=""M432 256C432 264.8 424.8 272 416 272h-176V448c0 8.844-7.156 16.01-16 16.01S208 456.8 208 448V272H32c-8.844 0-16-7.15-16-15.99C16 247.2 23.16 240 32 240h176V64c0-8.844 7.156-15.99 16-15.99S240 55.16 240 64v176H416C424.8 240 432 247.2 432 256z""/></svg>    
 Dodaj ogłoszenie
            </a>
            
                    </div>
                </div>
                <div class=""hidden xl:ml-6 xl:flex xl:items-center lg:space-x-1 2xl:space-x-4"">
                                                    <div class=""p-1"">
            <a
                href=""/praca""
                class="" text-gray-400 hover:text-gray-500 fade ""
                rel=""nofollow""
            >
                        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""search"" class=""svg-inline--fa fa-search text-gray-600 fa-lg"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M508.5 481.6l-129-129c-2.3-2.3-5.3-3.5-8.5-3.5h-10.3C395 312 416 262.5 416 208 416 93.1 322.9 0 208 0S0 93.1 0 208s93.1 208 208 208c54.5 0 104-21 141.1-55.2V371c0 3.2 1.3 6.2 3.5 8.5l129 129c4.7 4.7 12.3 4.7 17 0l9.9-9.9c4.7-4.7 4.7-12.3 0-17zM208 384c-97.3 0-176-78.7-176-176S110.7 32 208 32s176 78.7 176 176-78.7 176-176 176z""></path></svg>    

            </a>
            </div>
    
                                            <div class=""w-7 h-8 text-center"">
                <a
                    x-data=""observedCounter""
                    href=""/kandydat/praca-alert""
                    class=""hidden md:block p-1 text-gray-400 hover:text-gray-500 fade relative""
                    rel=""nofollow""
                >
                            
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  text-gray-600 fa-lg"" style="""" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M207.1 16C207.1 7.164 215.2 0 223.1 0C232.8 0 240 7.164 240 16V32.79C320.9 40.82 384 109 384 192V221.1C384 264.8 401.4 306.7 432.3 337.7L435 340.4C443.3 348.7 448 359.1 448 371.7C448 396.2 428.2 416 403.7 416H44.28C19.83 416 0 396.2 0 371.7C0 359.1 4.666 348.7 12.97 340.4L15.72 337.7C46.63 306.7 64 264.8 64 221.1V192C64 109 127.1 40.82 208 32.79L207.1 16zM223.1 64C153.3 64 95.1 121.3 95.1 192V221.1C95.1 273.3 75.26 323.4 38.35 360.3L35.6 363C33.29 365.3 31.1 368.5 31.1 371.7C31.1 378.5 37.5 384 44.28 384H403.7C410.5 384 416 378.5 416 371.7C416 368.5 414.7 365.3 412.4 363L409.7 360.3C372.7 323.4 352 273.3 352 221.1V192C352 121.3 294.7 64 223.1 64H223.1zM223.1 480C237.9 480 249.8 471.1 254.2 458.7C257.1 450.3 266.3 445.1 274.6 448.9C282.9 451.9 287.3 461 284.4 469.3C275.6 494.2 251.9 512 223.1 512C196.1 512 172.4 494.2 163.6 469.3C160.7 461 165.1 451.9 173.4 448.9C181.7 445.1 190.9 450.3 193.8 458.7C198.2 471.1 210.1 480 223.1 480z""/></svg>    

                    <template x-if=""jobAlertCount() > 0"">
                        <span
                            x-text=""jobAlertCount()""
                            class=""absolute -top-3 -right-2 badge badge-primary badge-sm"">
                        </span>
                    </template>
                </a>
            </div>
            
                                            <div class=""w-7 h-8 text-center"">
                <a
                    x-data=""observedCounter""
                    href=""/obserwowane/oferty""
                    class=""hidden md:block p-1 text-gray-400 hover:text-gray-500 fade relative""
                    rel=""nofollow""
                >
                            
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  text-gray-600 fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

                    <template x-if=""offerCount() > 0"">
                        <span
                            x-text=""offerCount()""
                            class=""absolute -top-3 -right-2 badge badge-primary badge-sm"">
                        </span>
                    </template>
                </a>
            </div>
            
                                                            <a href=""/kandydat/logowanie"" class=""btn btn-outline-brand whitespace-nowrap"" title="""" rel=""nofollow"">
            Konto kandydata
        </a>
        <a href=""/dla-pracodawcow"" class=""btn btn-outline-primary whitespace-nowrap"" title="""">
            Dla pracodawców
        </a>
    
                                                            </div>
                <div class=""xl:hidden flex items-center justify-center border border-solid border-gray-300 rounded-full hover:bg-gray-50 inline-block py-1 xl:py-0 pr-4 pl-4 xl:pl-0 xl:pr-1 my-2 sm:my-3"">
                    <button
                        @click.prevent=""mobileMenu = !mobileMenu""
                        type=""button""
                        class=""inline-flex items-center justify-center  text-gray-500 focus:outline-none focus:ring-0""
                        aria-controls=""mobile-menu"" aria-expanded=""false""
                    >
                                
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512"" class=""svg-inline--fa  fa-lg text-gray-500 mr-2"" style=""""><path fill=""currentColor"" d=""M0 88C0 74.75 10.75 64 24 64H424C437.3 64 448 74.75 448 88C448 101.3 437.3 112 424 112H24C10.75 112 0 101.3 0 88zM0 248C0 234.7 10.75 224 24 224H424C437.3 224 448 234.7 448 248C448 261.3 437.3 272 424 272H24C10.75 272 0 261.3 0 248zM424 432H24C10.75 432 0 421.3 0 408C0 394.7 10.75 384 24 384H424C437.3 384 448 394.7 448 408C448 421.3 437.3 432 424 432z""/></svg>    

                        Menu
                    </button>
                </div>
            </div>
        </div>

        <div
            x-cloak
            x-show=""mobileMenu""
            x-transition:enter=""transition ease-out duration-200""
            x-transition:enter-start=""opacity-0 scale-95""
            x-transition:enter-end=""opacity-100 scale-100""
            x-transition:leave=""duration-100 ease-in""
            x-transition:leave-start=""opacity-100 scale-y-100""
            x-transition:leave-end=""opacity-0 scale-95""
            class=""mobile-menu fixed top-0 inset-x-0 p-2 transition transform origin-top-right xl:hidden""
            id=""mobile-menu""
        >
            <div class=""rounded-lg shadow-lg ring-1 ring-black ring-opacity-5 bg-white divide-y-2 divide-gray-50"">
                <div class=""pt-2 pb-3 space-y-1"">
                    <div class=""flex items-center justify-between px-5"">
                        <div>
                            <img class=""h-8 w-auto"" width=""32"" height=""32"" src=""/build/favicons/favicon-32x32.5fce8ec3.png"" alt=""Aplikuj.pl"">
                        </div>
                        <div class=""-mr-2"">
                            <button
                                @click.prevent=""mobileMenu = !mobileMenu""
                                type=""button""
                                class=""bg-white rounded-md p-2 inline-flex items-center justify-center text-gray-400 hover:text-gray-500 hover:bg-gray-100 focus:outline-none focus:ring-0""
                            >
                                        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times"" class=""svg-inline--fa fa-times fa-2x"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512""><path fill=""currentColor"" d=""M193.94 256L296.5 153.44l21.15-21.15c3.12-3.12 3.12-8.19 0-11.31l-22.63-22.63c-3.12-3.12-8.19-3.12-11.31 0L160 222.06 36.29 98.34c-3.12-3.12-8.19-3.12-11.31 0L2.34 120.97c-3.12 3.12-3.12 8.19 0 11.31L126.06 256 2.34 379.71c-3.12 3.12-3.12 8.19 0 11.31l22.63 22.63c3.12 3.12 8.19 3.12 11.31 0L160 289.94 262.56 392.5l21.15 21.15c3.12 3.12 8.19 3.12 11.31 0l22.63-22.63c3.12-3.12 3.12-8.19 0-11.31L193.94 256z""></path></svg>    

                            </button>
                        </div>
                    </div>
                            <a href=""/praca""
           class=""nav-link active"" title="""">
            Oferty pracy
        </a>
        <a href=""/pracodawcy"" class=""nav-link"" title="""">
            Pracodawcy
        </a>
        <a href=""/porady"" class=""nav-link"" title="""">
            Porady
        </a>
        <a href=""/cv/"" class=""nav-link"" title="""">
            Kreator CV
        </a>
        <a href=""/kalkulator-wynagrodzen"" class=""nav-link"" title="""">
            Kalkulator wynagrodzeń
        </a>
                    <a href=""/dla-pracodawcow"" class=""nav-link special"" title="""">
                        
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512"" class=""svg-inline--fa fa-angle-down text-secondary fa-1x font-bold mr-1"" style=""""><path fill=""currentColor"" d=""M432 256C432 264.8 424.8 272 416 272h-176V448c0 8.844-7.156 16.01-16 16.01S208 456.8 208 448V272H32c-8.844 0-16-7.15-16-15.99C16 247.2 23.16 240 32 240h176V64c0-8.844 7.156-15.99 16-15.99S240 55.16 240 64v176H416C424.8 240 432 247.2 432 256z""/></svg>    
 Dodaj ogłoszenie
            </a>
            
                </div>
                <div class=""pt-4 pb-3 border-t border-gray-200"">
                                                                        <div class=""py-2 flex justify-around"">
                                        <a href=""/kandydat/logowanie"" class=""btn btn-outline-brand whitespace-nowrap"" title="""" rel=""nofollow"">
            Konto kandydata
        </a>
        <a href=""/dla-pracodawcow"" class=""btn btn-outline-primary whitespace-nowrap"" title="""">
            Dla pracodawców
        </a>
    
                            </div>
                                                            </div>
            </div>
        </div>

            </nav>

            </header>

    <main id=""page-content"">
                                
    
            
    
                <section
            class=""relative bg-gray-300 bg-cover bg-center bg-no-repeat overflow-hidden""
            style=""background-image: url(/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDFx8_o3PqAb4JxQGUzHeU0Cs7KT3hN_xvWnyAyJANgSCB4WBuE0SavCHDKsvXHE1LGSw5Ws1VFRjEjeD0arCp_8SKdelmcTnMhkjlA5E0798se9Z3Z_OFh1WV1fluCAGl642KpNFXSwkrvm2gsEE2hPCKErDP8g9uDm8U80aFjy35uD8Dg-mncXF4eFBrsinFM_m5EpAtade-tqpKb0yBI8VCAlids1Q4ih8T1MFPNoh0q9sjJwi6nckpqjZ5PAe_uCBbHg_Kbz8eHNeYFfqDPV6t6yx8lrlhC_O4IxNrC0S-JV3uGXLmzMtXr8OJMs99u-oPbEvByg5-e6JE6xRzoyJZwPUm3FxbJIhlIrcS_hHutn2gcF0UOMwaBOrkwsm1Ql6pMV3e1vUEDTQOV6J87hnas_gOBQmJwb9f4Hmy9Va1f6pt2dki5SJlv8F4UdSRM6FFHwQOqUI3Zcx68ES6CIg/173891605212758100.webp)""
    >
        <div class=""relative container max-w-7xl mx-auto z-0 h-full flex justify-center items-center text-center py-12 md:pt-20 md:pb-44 xl:pt-36 xl:pb-48 px-4 md:px-6 lg:px-8""></div>
    </section>

    <section
    class=""md:relative md:container md:max-w-7xl md:mx-auto md:-mt-20 md:mb-10""
    x-data=""offer""
>
    <div class=""flex flex-col bg-white md:shadow md:rounded-xl"">
        <div class=""order-1 px-3 md:px-8 flex flex-col lg:flex-row lg:items-center lg:justify-between"">
            
<nav class=""flex overflow-x-auto space-x-4 mt-2 sm:mt-4"" aria-label=""Breadcrumb"">
    <ol role=""list"" class=""flex items-center space-x-2 mb-2 sm:mb-0"">
        <li>
            <div class=""flex items-center whitespace-nowrap"">
                <a href=""/"" class=""py-1 text-sm font-light text-gray-500"">
                    Aplikuj.pl
                </a>
            </div>
        </li>
                    <li>
                <div class=""flex items-center whitespace-nowrap"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-right"" class=""svg-inline--fa fa-angle-right flex-shrink-0 text-gray-400"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 192 512""><path fill=""currentColor"" d=""M166.9 264.5l-117.8 116c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17L127.3 256 25.1 155.6c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0l117.8 116c4.6 4.7 4.6 12.3-.1 17z""></path></svg>
    

                    <a href=""/praca""
                       class=""ml-2 py-1 text-sm font-light text-gray-500"">
                        Praca
                    </a>
                </div>
            </li>
                    <li>
                <div class=""flex items-center whitespace-nowrap"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-right"" class=""svg-inline--fa fa-angle-right flex-shrink-0 text-gray-400"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 192 512""><path fill=""currentColor"" d=""M166.9 264.5l-117.8 116c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17L127.3 256 25.1 155.6c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0l117.8 116c4.6 4.7 4.6 12.3-.1 17z""></path></svg>
    

                    <a href=""/praca/slaskie""
                       class=""ml-2 py-1 text-sm font-light text-gray-500"">
                        Śląskie
                    </a>
                </div>
            </li>
                    <li>
                <div class=""flex items-center whitespace-nowrap"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-right"" class=""svg-inline--fa fa-angle-right flex-shrink-0 text-gray-400"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 192 512""><path fill=""currentColor"" d=""M166.9 264.5l-117.8 116c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17L127.3 256 25.1 155.6c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0l117.8 116c4.6 4.7 4.6 12.3-.1 17z""></path></svg>
    

                    <a href=""/praca/sosnowiec""
                       class=""ml-2 py-1 text-sm font-light text-gray-500"">
                        Sosnowiec
                    </a>
                </div>
            </li>
                    <li>
                <div class=""flex items-center whitespace-nowrap"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-right"" class=""svg-inline--fa fa-angle-right flex-shrink-0 text-gray-400"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 192 512""><path fill=""currentColor"" d=""M166.9 264.5l-117.8 116c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17L127.3 256 25.1 155.6c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0l117.8 116c4.6 4.7 4.6 12.3-.1 17z""></path></svg>
    

                    <a href=""/praca/sosnowiec/magazynier""
                       class=""ml-2 py-1 text-sm font-light text-gray-500"">
                        Magazynier
                    </a>
                </div>
            </li>
            </ol>
</nav>

                            <div class=""offer-list mt-2 sm:mt-4"">
                                            <span class=""offer-badge badge-recommended"">
                            Polecana
                        </span>
                                                                <span class=""offer-badge badge-popular"">
                            Popularna
                        </span>
                                    </div>
                    </div>

        <div
            id=""offer-container""
            class=""order-2 px-3 md:px-8 py-3 md:py-6 text-sm md:text-base""
        >
        
        
                    <div class=""grid grid-cols-1 xl:grid-cols-3 xl:gap-4"">

                <div class=""lg:col-span-2 offer-template"">
                                        
    <div class=""flex"">
        <div class=""flex-shrink-0 md:self-center"">
                                
                    <div class=""mr-2 mt-2 sm:mr-8 sm:mt-0"">
            <img
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtmisYuldmqYUeppGKqLwlzC0BdBiCEzIlpHmeMFHpzFCkz2Q0GhToNyDNtGOrwNWLrtsp4Poo3WZnRcDzrTWYWkMsBwnnklpP41ZfngpLa-m87faAjHUWtlbb3TF02kc6woW_dzCGoXtcqYaC9yeLbWBpiZZVAZGaHUqTwIvLycmeKuY0aL7WVKQslVQk61dIvu4cnZfCxCmES2zNCL6rP4ei8-pN4ok3s9LaEvdpzsDe6cg7gB2qwpRofUmtFjfEIwNO_N3m8HDl1msXBR1AJBinIYw8tyYeqrMEI332vVpaABec76wEn3mgvRr0Nyo5ZNbyBi2xTL5I27V2aVL6Uewatg/952f31ec324e2b01e08bf66a95797441.webp""
                width=""200""
                height=""200""
                class=""text-2xs inline-block w-36""
                alt=""Gi Group S.A.""
            >
        </div>
    
                    </div>
        <div class=""space-y-3 md:space-y-2"">
            <h1 class=""text-lg sm:text-xl lg:text-2xl 2xl:text-3xl leading-5 lg:leading-9"">
                                    Magazynier // 5.570 zł // od zaraz
    

            </h1>

            
                                            
                                                    
                
                    <div class=""text-md lg:text-base"">
                                                    <a href=""https://www.aplikuj.pl/pracodawca/warszawa/10423/gi-group-s-a-"" title=""Szczegóły pracodawcy Gi Group S.A."" class=""text-primary sm:hover:underline flex items-center"">
                                Gi Group S.A.&nbsp;
                                                                        
            <span class=""block sm:inline-block "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512"" class=""svg-inline--fa  text-secondary text-base"" style=""""><path fill=""currentColor"" d=""M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zm47-320.7l105.1 15.3-76.1 74.2 18 104.7L256 336l-94 49.4 18-104.7-76.1-74.2L209 191.3 256 96l47 95.3z""/></svg>    

        </span>
    
                                                            </a>
                                            </div>
            
                                
        
            
        
    <div class=""flex flex-col md:flex-row md:items-center md:gap-4"">
        <div class=""w-28 md:w-36"">
            <div class=""overflow-hidden rounded-full bg-gray-200"">
                <div
                    style=""width: 71%""
                    class=""h-2 rounded-full bg-primary-light"">
                </div>
            </div>
        </div>
        <div class=""mt-2 md:mt-0"">
            <span class=""text-xs md:text-sm text-gray-700 font-semibold"">
                wygasa za 9 dni
                <span class=""text-gray-500 font-normal"">
                    (do <time datetime=""2025-10-05"">5 paź</time>)
                </span>
            </span>
        </div>
    </div>

            </div>
</div>


                        
            <div class=""flex bg-gray-100 rounded-lg py-1 lg:py-2.5 px-2 lg:px-4 mt-4"">
            <div class=""w-8 text-center mr-4 mt-1 flex-shrink-0"">
                        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""usd-circle"" class=""svg-inline--fa fa-usd-circle fa-2xl text-gray-900 mt-1"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 496 512""><path fill=""currentColor"" d=""M248 8C111 8 0 119 0 256s111 248 248 248 248-111 248-248S385 8 248 8zm0 464c-119.1 0-216-96.9-216-216S128.9 40 248 40s216 96.9 216 216-96.9 216-216 216zm40.3-221.3l-72-20.2c-12.1-3.4-20.6-14.4-20.6-26.7 0-15.3 12.8-27.8 28.5-27.8h45c11.2 0 21.9 3.6 30.6 10.1 3.2 2.4 7.6 2 10.4-.8l11.3-11.5c3.4-3.4 3-9-.8-12-14.6-11.6-32.6-17.9-51.6-17.9H264v-40c0-4.4-3.6-8-8-8h-16c-4.4 0-8 3.6-8 8v40h-7.8c-33.3 0-60.5 26.8-60.5 59.8 0 26.6 18.1 50.2 43.9 57.5l72 20.2c12.1 3.4 20.6 14.4 20.6 26.7 0 15.3-12.8 27.8-28.5 27.8h-45c-11.2 0-21.9-3.6-30.6-10.1-3.2-2.4-7.6-2-10.4.8l-11.3 11.5c-3.4 3.4-3 9 .8 12 14.6 11.6 32.6 17.9 51.6 17.9h5.2v40c0 4.4 3.6 8 8 8h16c4.4 0 8-3.6 8-8v-40h7.8c33.3 0 60.5-26.8 60.5-59.8-.1-26.6-18.1-50.2-44-57.5z""></path></svg>    

            </div>
            <ul role=""list"" class=""flex flex-col"">
                                    
                        
                        <li class=""py-1 flex"">
                            <div class=""flex flex-col"">
                                <span class=""block text-sm text-gray-700"">Umowa o pracę</span>
                                <div>
                                    <span class=""font-extrabold"">4 do 5 570 zł</span>
                                    <span class=""text-sm"">
                                        brutto /
                                        mies.
                                    </span>
                                </div>
                            </div>
                        </li>

                                                </ul>
        </div>
    

                    <div class=""pt-8"">
                            
        
                        
    <div class=""grid grid-col-1 sm:grid-cols-2 gap-x-8 gap-y-3"">
        <div>
            <ul>
                                    <li class=""flex items-center pt-1"">
                        <div class=""w-12 h-12 bg-gray-100 rounded-lg inline-flex items-center justify-center mr-3 flex-shrink-0"">
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt fa-xl"" style=""color: #"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

                        </div>
                        <div>
                            <span class=""text-gray-500"">
                                <span class=""block"">
                                    Sosnowiec, śląskie</span>
                                                                                                    </span>
                        </div>
                    </li>
                            </ul>
        </div>
        <div class=""offer__label-wrapper"">
                            <div class=""relative flex items-center space-x-3 offer__label"">
                    <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""user"" class=""svg-inline--fa fa-user fa-xl"" style=""color: #"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M313.6 288c-28.7 0-42.5 16-89.6 16-47.1 0-60.8-16-89.6-16C60.2 288 0 348.2 0 422.4V464c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48v-41.6c0-74.2-60.2-134.4-134.4-134.4zM416 464c0 8.8-7.2 16-16 16H48c-8.8 0-16-7.2-16-16v-41.6C32 365.9 77.9 320 134.4 320c19.6 0 39.1 16 89.6 16 50.4 0 70-16 89.6-16 56.5 0 102.4 45.9 102.4 102.4V464zM224 256c70.7 0 128-57.3 128-128S294.7 0 224 0 96 57.3 96 128s57.3 128 128 128zm0-224c52.9 0 96 43.1 96 96s-43.1 96-96 96-96-43.1-96-96 43.1-96 96-96z""></path></svg>    

                    </span>
                    </div>
                    <div class=""flex-1"">
                        <p class=""text-gray-500"">
                            Magazynier
                        </p>
                    </div>
                </div>
                    </div>
    </div>

                    </div>
                        
    
    

                                    
                
                
                
    
    
    
    
    
    
                
                
                
    
        
    <div class=""grid grid-col-1 sm:grid-cols-2 gap-x-8 gap-y-3 py-3 md:pb-8 offer__label-wrapper""><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-signature"" class=""svg-inline--fa fa-file-signature fa-xl"" style=""color: #"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M560.83 135.96l-24.79-24.79c-20.23-20.24-53-20.26-73.26 0L384 189.72v-57.75c0-12.7-5.1-25-14.1-33.99L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99v-80.54c6.29-4.68 12.62-9.35 18.18-14.95l158.64-159.3c9.79-9.78 15.17-22.79 15.17-36.63s-5.38-26.84-15.16-36.63zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v61.53l-48.51 48.24c-30.14 29.96-47.42 71.51-47.47 114-3.93-.29-7.47-2.42-9.36-6.27-11.97-23.86-46.25-30.34-66-14.17l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L103 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H80c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L144 354.58l16.81 50.48c4.54 13.51 23.14 14.83 29.5 2.08l7.66-15.33c4.01-8.07 15.8-8.59 20.22.34C225.44 406.61 239.9 415.7 256 416h32c22.05-.01 43.95-4.9 64.01-13.6v61.61zm27.48-118.05A129.012 129.012 0 0 1 288 384v-.03c0-34.35 13.7-67.29 38.06-91.51l120.55-119.87 52.8 52.8-119.92 120.57zM538.2 186.6l-21.19 21.19-52.8-52.8 21.2-21.19c7.73-7.73 20.27-7.74 28.01 0l24.79 24.79c7.72 7.73 7.72 20.27-.01 28.01z""></path></svg>    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Umowa o pracę
                    </p>
                </div>
            </div><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""chart-pie-alt"" class=""svg-inline--fa fa-chart-pie-alt fa-xl"" style=""color: #"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M461.29 288H224V50.71c0-8.83-7.18-16.21-15.74-16.21-.69 0-1.4.05-2.11.15C87.08 51.47-3.96 155.43.13 280.07 4.2 404.1 107.91 507.8 231.93 511.87c2.69.09 5.39.13 8.07.13 121.04 0 220.89-89.66 237.35-206.16 1.33-9.45-6.52-17.84-16.06-17.84zM240 480c-2.33 0-4.68-.04-7.02-.12-107.24-3.52-197.35-93.63-200.87-200.87C28.84 179.02 96.45 92.23 192 69.83V320h250.15C420.27 412.43 336.49 480 240 480zM288.8.04c-.35-.03-.7-.04-1.04-.04C279.1 0 272 7.44 272 16.23V240h223.77c9.14 0 16.82-7.69 16.2-16.8C503.72 103.74 408.26 8.28 288.8.04zM304 208V33.9c89.25 13.81 160.28 84.85 174.1 174.1H304z""></path></svg>    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Pełny etat
                    </p>
                </div>
            </div><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""shield"" class=""svg-inline--fa fa-shield fa-xl"" style=""color: #"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M466.5 83.7l-192-80a48.15 48.15 0 0 0-36.9 0l-192 80C27.7 91.1 16 108.6 16 128c0 198.5 114.5 335.7 221.5 380.3 11.8 4.9 25.1 4.9 36.9 0C360.1 472.6 496 349.3 496 128c0-19.4-11.7-36.9-29.5-44.3zM262.2 478.8c-3.9 1.6-8.3 1.6-12.3 0C152 440 48 304 48 128c0-6.5 3.9-12.3 9.8-14.8l192-80c3.8-1.6 8.3-1.7 12.3 0l192 80c6 2.5 9.8 8.3 9.8 14.8.1 176-103.9 312-201.7 350.8z""></path></svg>    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Pracownik fizyczny
                    </p>
                </div>
            </div><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""building"" class=""svg-inline--fa fa-building fa-xl"" style=""color: #"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M192 107v40c0 6.627-5.373 12-12 12h-40c-6.627 0-12-5.373-12-12v-40c0-6.627 5.373-12 12-12h40c6.627 0 12 5.373 12 12zm116-12h-40c-6.627 0-12 5.373-12 12v40c0 6.627 5.373 12 12 12h40c6.627 0 12-5.373 12-12v-40c0-6.627-5.373-12-12-12zm-128 96h-40c-6.627 0-12 5.373-12 12v40c0 6.627 5.373 12 12 12h40c6.627 0 12-5.373 12-12v-40c0-6.627-5.373-12-12-12zm128 0h-40c-6.627 0-12 5.373-12 12v40c0 6.627 5.373 12 12 12h40c6.627 0 12-5.373 12-12v-40c0-6.627-5.373-12-12-12zm-128 96h-40c-6.627 0-12 5.373-12 12v40c0 6.627 5.373 12 12 12h40c6.627 0 12-5.373 12-12v-40c0-6.627-5.373-12-12-12zm128 0h-40c-6.627 0-12 5.373-12 12v40c0 6.627 5.373 12 12 12h40c6.627 0 12-5.373 12-12v-40c0-6.627-5.373-12-12-12zm140 205v20H0v-20c0-6.627 5.373-12 12-12h20V24C32 10.745 42.745 0 56 0h336c13.255 0 24 10.745 24 24v456h20c6.627 0 12 5.373 12 12zm-64-12V32H64v448h128v-85c0-6.627 5.373-12 12-12h40c6.627 0 12 5.373 12 12v85h128z""></path></svg>    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Praca stacjonarna
                    </p>
                </div>
            </div><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  fa-xl"" style=""color: #""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Praca od zaraz
                    </p>
                </div>
            </div><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  fa-xl"" style=""color: #""><path fill=""currentColor"" d=""M638.9 209.7l-8-13.9c-2.2-3.8-7.1-5.1-10.9-2.9l-108 63V240c0-26.5-21.5-48-48-48H320v62.2c0 16-10.9 30.8-26.6 33.3-20 3.3-37.4-12.2-37.4-31.6v-94.3c0-13.8 7.1-26.6 18.8-33.9l33.4-20.9c11.4-7.1 24.6-10.9 38.1-10.9h103.2l118.5-67c3.8-2.2 5.2-7.1 3-10.9l-8-14c-2.2-3.8-7.1-5.2-10.9-3l-111 63h-94.7c-19.5 0-38.6 5.5-55.1 15.8l-33.5 20.9c-17.5 11-28.7 28.6-32.2 48.5l-62.5 37c-21.6 13-35.1 36.7-35.1 61.9v38.6L4 357.1c-3.8 2.2-5.2 7.1-3 10.9l8 13.9c2.2 3.8 7.1 5.2 10.9 3L160 305.3v-57.2c0-14 7.5-27.2 19.4-34.4l44.6-26.4v65.9c0 33.4 24.3 63.3 57.6 66.5 38.1 3.7 70.4-26.3 70.4-63.7v-32h112c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16h-24v36c0 19.8-16 35.8-35.8 35.8h-16.1v16c0 22.2-18 40.2-40.2 40.2H238.5l-115.6 67.3c-3.8 2.2-5.1 7.1-2.9 10.9l8 13.8c2.2 3.8 7.1 5.1 10.9 2.9L247.1 448h100.8c34.8 0 64-24.8 70.8-57.7 30.4-6.7 53.3-33.9 53.3-66.3v-4.7c13.6-2.3 24.6-10.6 31.8-21.7l132.2-77c3.8-2.2 5.1-7.1 2.9-10.9z""/></svg>    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Bez doświadczenia
                    </p>
                </div>
            </div><div class=""relative flex items-center space-x-3 offer__label"">
                <div class=""flex-shrink-0"">
                    <span class=""rounded-lg inline-flex icon-wrapper w-12 h-12 items-center justify-center bg-gray-100"">
                                
            <svg class=""svg-inline--fa fa-angle-left fa-xl"" role=""img"" viewBox=""0 0 21 20"" fill=""none"" xmlns=""https://www.w3.org/2000/svg""><g clip-path=""url(#clip0_2_4892)""><rect x=""0.300171"" y=""2"" width=""20"" height=""8"" fill=""#005BBB""/><rect x=""0.300171"" y=""10"" width=""20"" height=""8"" fill=""#FFD500""/></g><defs><clipPath id=""clip0_2_4892""><rect width=""20"" height=""20"" fill=""white"" transform=""translate(0.300171)""/></clipPath></defs></svg>
    

                    </span>
                </div>
                <div class=""flex-1"">
                    <p class=""text-gray-500"">
                        Запрошуємо працівників з України
                    </p>
                </div>
            </div></div>


                        
                    <div class=""w-full border-t border-gray-200 mt-4""></div>

                                                        <img
                class=""object-cover rounded-lg mt-4""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbV5QNHJUHtP4xPdgSc0IghvTi9-WB-F0z_tVSOKoODDTAWMS0JM7RQXRn0jJnFCvipl8SWeagHCVC5_3DkTzx8vpY4M9NaIqrEhvHc7JgTWADl9vCPAJhuClwakzWtxRCKrdn6ctSmr04Xc7dgp0uIt1DADWZyk8H3lSUQuAwer0HYZuAsStR9afKzivPfCjnsO1VLS8RE4lV10iuHGOhyGsElg5cmYuW_mFC1nlppLCtbSHRnNn5PSw5zKaq8iIfuFGadr2lVxuUBIpO4cnMaVmWfOSGecQLv0es_z8rBN8N4r-pCGTaZ4gI7OgZlkmlLo0JUEJFbiXV-HJQZO_ay02Sb3zmljHAwWYFrOaog-oGFXpeQHcS3kvDYCHFvIrddKjH63-lP5YzVsJ8TzanWj/173891615367965600.webp""
                width=""915""
                height=""385""
                loading=""lazy""
                alt=""Magazynier // 5.570 zł // od zaraz""
            >
            
                                                            <div class=""leading-6 py-6"">
                 
            </div>
        
    
                
            <div class=""leading-6 py-6"">
            <p>Poszukujemy Pracownik&oacute;w Magazynowych! (bez doświadczenia)Lokalizacja: Sosnowiec???? Zainteresowany/a? Dzwoń już teraz!???? +48 782- 806- 161Administratorem Danych Osobowych jest Gi Group Poland S.A., z siedzibą w Warszawie, ul. Sienna 75, 00-833 Warszawa oraz podmioty wskazane w Polityce Prywatności. Z Inspektorem Ochrony Danych Osobowych można skontaktować używając adresu: iod(at)gigroup.com lub pisemnie na adres siedziby. Dane osobowe będą przetwarzane w celu realizacji procesu rekrutacji (podstawa prawna: art. 22(1) &sect; 1 ustawy z dnia 26.06.1974 r. - Kodeks pracy w zw. z art. 6 ust. 1 lit. c lub lit. a (w zakresie przetwarzania danych w oparciu o zgodę).Rozporządzenia z dnia 27 kwietnia 2016 r. Rozporządzenie RODO w ramach realizacji obowiązku prawnego ciążącego na administratorze danych. Podanie danych oraz wyrażenie zgody na ich przetwarzanie jest dobrowolne, ale konieczne do wzięcia udziału w prowadzonej rekrutacji. Czas przechowywania danych: powierzone dane osobowe będą przechowywane do czasu prowadzonych rekrutacji - nie dłużej niż 48 miesięcy od ostatniej aktywności użytkownika albo do momentu odwołania wyrażonej zgody. Przewidywane kategorie odbiorc&oacute;w danych: osoby zajmujące się rekrutacją oraz decydujące o zatrudnieniu, dział kadr i płac oraz osoby odpowiadające za nadz&oacute;r IT, nadz&oacute;r nad poprawnością działań rekrutacyjnych w tym prawnicy. Przysługujące prawa: masz prawo do żądania od administratora dostępu do danych osobowych dotyczących swojej osoby, ich sprostowania, usunięcia lub ograniczenia przetwarzania, cofnięcia wyrażonej zgody, a także prawo wniesienia sprzeciwu wobec przetwarzania danych oraz prawo do wniesienia skargi do organu nadzorczego.<br />Pełną informację odnośnie przetwarzania Twoich danych osobowych znajdziesz pod adresem: .<br /><br />Informujemy, że wewnętrzna procedura dokonywania zgłoszeń naruszeń prawa i podejmowania działań następczych (Procedura dot. zgłoszeń sygnalist&oacute;w) jest dostępna na stronie internetowej pod następującym adresem  Zgłoszeń w trybie przewidzianym w Procedurze dot. zgłoszeń sygnalist&oacute;w można dokonać pod następującym adresem: <br />Gi Group jest jedną z największych agencji pracy i doradztwa personalnego na świecie. Firma zapewnia kompleksowe usługi w zakresie rekrutacji pracownik&oacute;w wszystkich szczebli, stałego i czasowego zatrudnienia oraz outsourcingu. Nr wpisu do Rejestru Agencji Zatrudnienia: 2010</p>
        </div>
    
    
                            
        
    
                        
    <div class=""pt-6"">
                <h2 class=""pb-2 font-bold"">
        Zakres obowiązków
    </h2>
    <div class=""border-t-4 border-gray-300 w-20 mb-4"" style=""border-color: #d1d5db""></div>

        <div class=""pb-4 sm:pb-12"">
                        
    
    
    

    
    <p>Zadania:</p>
<ul>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Kompletacja zam&oacute;wień</div></li>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Przyjmowanie towaru</div></li>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Wydawanie towaru</div></li>
</ul>
<p><br /><br /> <br /><br /></p>



        </div>
    
                <h2 class=""pb-2 font-bold"">
        Wymagania
    </h2>
    <div class=""border-t-4 border-gray-300 w-20 mb-4"" style=""border-color: #d1d5db""></div>

        <div class=""pb-4 sm:pb-12"">
                        
    
    
    

    
    <p>Oczekujemy:</p>
<ul>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Dyspozycyjności do pracy w systemie 3- zmianowym</div></li>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Zaangażowania w wykonywane obowiązki</div></li>
</ul>



        </div>
    
                <h2 class=""pb-2 font-bold"">
        Oferujemy
    </h2>
    <div class=""border-t-4 border-gray-300 w-20 mb-4"" style=""border-color: #d1d5db""></div>

        <div class=""pb-4 sm:pb-12"">
                        
    
    
    

    
    <br/>
<ul>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Stabilne zatrudnienie na podstawie umowy o pracę</div></li>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Wynagrodzenie: 5570 zł brutto (podstawa + premia)</div></li>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Premię do 15% wynagrodzenia zasadniczego</div></li>
<li class=""leading-6 flex py-1"">
            <div class=""mr-4 flex-shrink-0"">        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1"" style="""" role=""img"" xmlns="""" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    
</div><div class=""flex-auto"">Pracę w systemie 3- zmianowym od poniedziałku do piątku</div></li>
</ul>



        </div>
        </div>


                                    
    
                
    
                        
    <div class=""mt-10 mb-6"">
        <h5 class=""text-base font-medium md:text-lg"">
            Dowiedz się więcej o pracodawcy
        </h5>
                <div class=""text-left"">
                    
    <div class=""flex justify-start space-x-4 py-2"">                <a
                    href=""https://pl.gigroup.com/""
                    class=""flex items-center justify-center h-12 w-12 rounded-md bg-gray-100 hover:shadow hover:bg-gray-50""
                    rel=""nofollow""
                    target=""_blank""
                >
                        
                        
            
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512"" class=""svg-inline--fa  fa-2x"" style=""color: #050f2c""><path fill=""currentColor"" d=""M0 256C0 167.6 71.6 96 160 96h80c8.8 0 16 7.2 16 16s-7.2 16-16 16H160C89.3 128 32 185.3 32 256s57.3 128 128 128h80c8.8 0 16 7.2 16 16s-7.2 16-16 16H160C71.6 416 0 344.4 0 256zm576 0c0 88.4-71.6 160-160 160H336c-8.8 0-16-7.2-16-16s7.2-16 16-16h80c70.7 0 128-57.3 128-128s-57.3-128-128-128H336c-8.8 0-16-7.2-16-16s7.2-16 16-16h80c88.4 0 160 71.6 160 160zM152 240H424c8.8 0 16 7.2 16 16s-7.2 16-16 16H152c-8.8 0-16-7.2-16-16s7.2-16 16-16z""/></svg>    


                </a>
                            <a
                    href=""http://www.youtube.com/@GiGroupPolska/""
                    class=""flex items-center justify-center h-12 w-12 rounded-md bg-gray-100 hover:shadow hover:bg-gray-50""
                    rel=""nofollow""
                    target=""_blank""
                >
                        
                        
            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""youtube"" class=""svg-inline--fa fa-youtube fa-2x"" style=""color: #ff0000"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M549.655 124.083c-6.281-23.65-24.787-42.276-48.284-48.597C458.781 64 288 64 288 64S117.22 64 74.629 75.486c-23.497 6.322-42.003 24.947-48.284 48.597-11.412 42.867-11.412 132.305-11.412 132.305s0 89.438 11.412 132.305c6.281 23.65 24.787 41.5 48.284 47.821C117.22 448 288 448 288 448s170.78 0 213.371-11.486c23.497-6.321 42.003-24.171 48.284-47.821 11.412-42.867 11.412-132.305 11.412-132.305s0-89.438-11.412-132.305zm-317.51 213.508V175.185l142.739 81.205-142.739 81.201z""></path></svg>    


                </a>
                            <a
                    href=""http://www.linkedin.com/company/gi-group/""
                    class=""flex items-center justify-center h-12 w-12 rounded-md bg-gray-100 hover:shadow hover:bg-gray-50""
                    rel=""nofollow""
                    target=""_blank""
                >
                        
                        
            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""linkedin-in"" class=""svg-inline--fa fa-linkedin-in fa-2x"" style=""color: #0077b5"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M100.28 448H7.4V148.9h92.88zM53.79 108.1C24.09 108.1 0 83.5 0 53.8a53.79 53.79 0 0 1 107.58 0c0 29.7-24.1 54.3-53.79 54.3zM447.9 448h-92.68V302.4c0-34.7-.7-79.2-48.29-79.2-48.29 0-55.69 37.7-55.69 76.7V448h-92.78V148.9h89.08v40.8h1.3c12.4-23.5 42.69-48.3 87.88-48.3 94 0 111.28 61.9 111.28 142.3V448z""></path></svg>    


                </a>
                            <a
                    href=""http://www.facebook.com/gigrouppl""
                    class=""flex items-center justify-center h-12 w-12 rounded-md bg-gray-100 hover:shadow hover:bg-gray-50""
                    rel=""nofollow""
                    target=""_blank""
                >
                        
                        
            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""facebook"" class=""svg-inline--fa fa-facebook fa-2x"" style=""color: #1877f2"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M504 256C504 119 393 8 256 8S8 119 8 256c0 123.78 90.69 226.38 209.25 245V327.69h-63V256h63v-54.64c0-62.15 37-96.48 93.67-96.48 27.14 0 55.52 4.84 55.52 4.84v61h-31.28c-30.8 0-40.41 19.12-40.41 38.73V256h68.78l-11 71.69h-57.78V501C413.31 482.38 504 379.78 504 256z""></path></svg>    


                </a>
            </div>

        </div>
    </div>


                                            
    <div class=""mb-8"">    
</div>
                    
                        
                
            <style>
            .clauses .space-y-2 > br {
                margin: 0 !important;
            }
        </style>
        <ul class=""sm:pb-12 min-h-12 sm:min-h-28 clauses border-t border-gray-200 text-sm"">
                            


<li class=""relative border-b border-gray-200"" x-data=""{ selected:null }"">

    
    <button
        type=""button""
        class=""w-full px-4 py-4 text-left""
        @click=""selected !== 1 ? selected = 1 : selected = null""
    >
        <span class=""flex items-center justify-between gap-x-4"">
            <span class=""font-semibold text-base md:text-lg"">
                Klauzula informacyjna
                            </span>

            <span
                class=""bg-indigo-50 w-8 h-8 md:w-9 md:h-9 rounded-md flex flex-shrink-0 items-center justify-center"">
                <template x-if=""selected !== 1"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-down"" class=""svg-inline--fa fa-angle-down fa-2x text-gray-600"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 256 512""><path fill=""currentColor"" d=""M119.5 326.9L3.5 209.1c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0L128 287.3l100.4-102.2c4.7-4.7 12.3-4.7 17 0l7.1 7.1c4.7 4.7 4.7 12.3 0 17L136.5 327c-4.7 4.6-12.3 4.6-17-.1z""></path></svg>
    

                </template>

                <template x-if=""selected === 1"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-up"" class=""svg-inline--fa fa-angle-up fa-2x text-gray-600"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 256 512""><path fill=""currentColor"" d=""M136.5 185.1l116 117.8c4.7 4.7 4.7 12.3 0 17l-7.1 7.1c-4.7 4.7-12.3 4.7-17 0L128 224.7 27.6 326.9c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17l116-117.8c4.7-4.6 12.3-4.6 17 .1z""></path></svg>
    

                </template>
            </span>
        </span>
    </button>

    <div class=""relative overflow-hidden transition-all max-h-0 duration-500""
         x-ref=""container1""
         :style=""selected === 1 ? 'max-height: ' + $refs.container1.scrollHeight + 'px' : ''""
    >
        <div class=""px-4 pt-2 pb-6 space-y-2 ul-list text-gray-600 lg:leading-7"">
            <p>Administratorem Danych Osobowych jest Gi Group Poland S.A., z siedzibą w Warszawie, ul. Sienna 75, 00-833 Warszawa oraz podmioty wskazane w Polityce Prywatności. Z Inspektorem Ochrony Danych Osobowych można skontaktować używając adresu: iod(at)gigroup.com lub pisemnie na adres siedziby. Dane osobowe będą przetwarzane w celu realizacji procesu rekrutacji (podstawa prawna: art. 22(1) &sect; 1 ustawy z dnia 26.06.1974 r. - Kodeks pracy w zw. z art. 6 ust. 1 lit. c lub lit. a (w zakresie przetwarzania danych w oparciu o zgodę).Rozporządzenia z dnia 27 kwietnia 2016 r. Rozporządzenie RODO w ramach realizacji obowiązku prawnego ciążącego na administratorze danych. Podanie danych oraz wyrażenie zgody na ich przetwarzanie jest dobrowolne, ale konieczne do wzięcia udziału w prowadzonej rekrutacji. Czas przechowywania danych: powierzone dane osobowe będą przechowywane do czasu prowadzonych rekrutacji - nie dłużej niż 48 miesięcy od ostatniej aktywności użytkownika albo do momentu odwołania wyrażonej zgody. Przewidywane kategorie odbiorc&oacute;w danych: osoby zajmujące się rekrutacją oraz decydujące o zatrudnieniu, dział kadr i płac oraz osoby odpowiadające za nadz&oacute;r IT, nadz&oacute;r nad poprawnością działań rekrutacyjnych w tym prawnicy. Przysługujące prawa: masz prawo do żądania od administratora dostępu do danych osobowych dotyczących swojej osoby, ich sprostowania, usunięcia lub ograniczenia przetwarzania, cofnięcia wyrażonej zgody, a także prawo wniesienia sprzeciwu wobec przetwarzania danych oraz prawo do wniesienia skargi do organu nadzorczego.<br />Pełną informację odnośnie przetwarzania Twoich danych osobowych znajdziesz pod adresem: https://pl.gigroup.com/polityka-prywatnosci/.<br /><br />Informujemy, że wewnętrzna procedura dokonywania zgłoszeń naruszeń prawa i podejmowania działań następczych (Procedura dot. zgłoszeń sygnalist&oacute;w) jest dostępna na stronie internetowej pod następującym adresem https://pl.gigroup.com/dla-pracownikow/sygnalisci Zgłoszeń w trybie przewidzianym w Procedurze dot. zgłoszeń sygnalist&oacute;w można dokonać pod następującym adresem: https://gigroupholding.vco.ey.com/<br /><br /><br />Gi Group jest jedną z największych agencji pracy i doradztwa personalnego na świecie. Firma zapewnia kompleksowe usługi w zakresie rekrutacji pracownik&oacute;w wszystkich szczebli, stałego i czasowego zatrudnienia oraz outsourcingu. Nr wpisu do Rejestru Agencji Zatrudnienia: 2010</p>
        </div>
    </div>

    
</li>

            
                            


<li class=""relative border-b border-gray-200"" x-data=""{ selected:null }"">

    
    <button
        type=""button""
        class=""w-full px-4 py-4 text-left""
        @click=""selected !== 2 ? selected = 2 : selected = null""
    >
        <span class=""flex items-center justify-between gap-x-4"">
            <span class=""font-semibold text-base md:text-lg"">
                Zgoda na przetwarzanie danych osobowych
                            </span>

            <span
                class=""bg-indigo-50 w-8 h-8 md:w-9 md:h-9 rounded-md flex flex-shrink-0 items-center justify-center"">
                <template x-if=""selected !== 2"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-down"" class=""svg-inline--fa fa-angle-down fa-2x text-gray-600"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 256 512""><path fill=""currentColor"" d=""M119.5 326.9L3.5 209.1c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0L128 287.3l100.4-102.2c4.7-4.7 12.3-4.7 17 0l7.1 7.1c4.7 4.7 4.7 12.3 0 17L136.5 327c-4.7 4.6-12.3 4.6-17-.1z""></path></svg>
    

                </template>

                <template x-if=""selected === 2"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-up"" class=""svg-inline--fa fa-angle-up fa-2x text-gray-600"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 256 512""><path fill=""currentColor"" d=""M136.5 185.1l116 117.8c4.7 4.7 4.7 12.3 0 17l-7.1 7.1c-4.7 4.7-12.3 4.7-17 0L128 224.7 27.6 326.9c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17l116-117.8c4.7-4.6 12.3-4.6 17 .1z""></path></svg>
    

                </template>
            </span>
        </span>
    </button>

    <div class=""relative overflow-hidden transition-all max-h-0 duration-500""
         x-ref=""container2""
         :style=""selected === 2 ? 'max-height: ' + $refs.container2.scrollHeight + 'px' : ''""
    >
        <div class=""px-4 pt-2 pb-6 space-y-2 ul-list text-gray-600 lg:leading-7"">
            <p>Wyrażam zgodę na przetwarzanie moich danych osobowych przez&nbsp;Gi Group Poland S.A., z siedzibą w Warszawie, ul. Sienna 75, 00-833 Warszawa oraz podmioty wskazane w Polityce Prywatności&nbsp;zawartych w załączonych dokumentach aplikacyjnych (w tym wizerunku), na potrzeby bieżącej rekrutacji. Zgoda jest dobrowolna i może być w każdym czasie wycofana. Dodatkowo wyrażam zgodę na przetwarzanie moich danych osobowych zawartych w załączonych dokumentach aplikacyjnych (w tym wizerunku), na potrzeby przyszłych rekrutacji przez okres 12 miesięcy. Zgoda jest dobrowolna i może być w każdym czasie wycofana.</p>
        </div>
    </div>

    
</li>

            
                            


<li class=""relative border-b border-gray-200"" x-data=""{ selected:null }"">

    
    <button
        type=""button""
        class=""w-full px-4 py-4 text-left""
        @click=""selected !== 3 ? selected = 3 : selected = null""
    >
        <span class=""flex items-center justify-between gap-x-4"">
            <span class=""font-semibold text-base md:text-lg"">
                Ustawa o sygnalistach
                            </span>

            <span
                class=""bg-indigo-50 w-8 h-8 md:w-9 md:h-9 rounded-md flex flex-shrink-0 items-center justify-center"">
                <template x-if=""selected !== 3"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-down"" class=""svg-inline--fa fa-angle-down fa-2x text-gray-600"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 256 512""><path fill=""currentColor"" d=""M119.5 326.9L3.5 209.1c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0L128 287.3l100.4-102.2c4.7-4.7 12.3-4.7 17 0l7.1 7.1c4.7 4.7 4.7 12.3 0 17L136.5 327c-4.7 4.6-12.3 4.6-17-.1z""></path></svg>
    

                </template>

                <template x-if=""selected === 3"">
                            
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-up"" class=""svg-inline--fa fa-angle-up fa-2x text-gray-600"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 256 512""><path fill=""currentColor"" d=""M136.5 185.1l116 117.8c4.7 4.7 4.7 12.3 0 17l-7.1 7.1c-4.7 4.7-12.3 4.7-17 0L128 224.7 27.6 326.9c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17l116-117.8c4.7-4.6 12.3-4.6 17 .1z""></path></svg>
    

                </template>
            </span>
        </span>
    </button>

    <div class=""relative overflow-hidden transition-all max-h-0 duration-500""
         x-ref=""container3""
         :style=""selected === 3 ? 'max-height: ' + $refs.container3.scrollHeight + 'px' : ''""
    >
        <div class=""px-4 pt-2 pb-6 space-y-2 ul-list text-gray-600 lg:leading-7"">
            <p>Informujemy, że wewnętrzna procedura dokonywania zgłoszeń naruszeń prawa i podejmowania działań następczych (Procedura dot. zgłoszeń sygnalist&oacute;w) jest dostępna na stronie internetowej pod następującym adresem&nbsp;<a href=""https://pl.gigroup.com/dla-pracownikow/sygnalisci"" target=""_blank"" data-saferedirecturl=""https://www.google.com/url?q=https://pl.gigroup.com/dla-pracownikow/sygnalisci&amp;source=gmail&amp;ust=1738919989510000&amp;usg=AOvVaw1ocygqcHenmvMD5lKDKLyM"" rel='nofollow'>https://pl.gigroup.com/dla-pracownikow/sygnalisci</a>&nbsp;Zgłoszeń w trybie przewidzianym w Procedurze dot. zgłoszeń sygnalist&oacute;w można dokonać pod następującym adresem:&nbsp;<a href=""https://gigroupholding.vco.ey.com/"" target=""_blank"" data-saferedirecturl=""https://www.google.com/url?q=https://gigroupholding.vco.ey.com/&amp;source=gmail&amp;ust=1738919989510000&amp;usg=AOvVaw0QS7_qDl-92yYI0QkG3Ak3"" rel='nofollow'>https://gigroupholding.vco.ey.com/</a></p>
        </div>
    </div>

    
</li>

                    </ul>
    

                    
                                                                                                        <div class=""text-center hidden lg:block"">    
            
                            
        <button
            type=""button""
            class=""btn btn-brand btn-apply inline-flex items-center btn-xl !px-36""
            x-data=""{ clicked: false }""
            :disabled=""clicked""
                                @click=""if(!clicked) {
                        clicked = true;
                        externalApplyHandleClick('https://pl.mygigroup.com/members/jobs/seek/viewoffer/38765');
                    }""
                    >
            Aplikuj
        </button>
    </div>
                                                    
                                                                                <div class=""flex flex-row items-center justify-between mt-12"">
                                        
    <div class=""flex flex-col xl:flex-row xl:items-center offer__dates"">
        <div class=""flex items-center mr-4"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""calendar-alt"" class=""svg-inline--fa fa-calendar-alt fa-md text-gray-400 mr-1"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M400 64h-48V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H128V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H48C21.5 64 0 85.5 0 112v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V112c0-26.5-21.5-48-48-48zM48 96h352c8.8 0 16 7.2 16 16v48H32v-48c0-8.8 7.2-16 16-16zm352 384H48c-8.8 0-16-7.2-16-16V192h384v272c0 8.8-7.2 16-16 16zM148 320h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm-96 96h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm-96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm192 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12z""></path></svg>    

            <span class=""text-gray-500 text-xs"">
                Dodana <span class=""text-gray-700"">
                    <time datetime=""2025-09-05"">5 września 2025</time>
                </span>
            </span>
        </div>
        <div class=""flex items-center mt-1 xl:mt-0"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""calendar-times"" class=""svg-inline--fa fa-calendar-times fa-md text-gray-400 mr-1"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M400 64h-48V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H128V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H48C21.5 64 0 85.5 0 112v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V112c0-26.5-21.5-48-48-48zM48 96h352c8.8 0 16 7.2 16 16v48H32v-48c0-8.8 7.2-16 16-16zm352 384H48c-8.8 0-16-7.2-16-16V192h384v272c0 8.8-7.2 16-16 16zm-105.3-95.9c4.7 4.7 4.7 12.3 0 17l-5.7 5.7c-4.7 4.7-12.3 4.7-17 0l-48-48.2-48.1 48.1c-4.7 4.7-12.3 4.7-17 0l-5.7-5.7c-4.7-4.7-4.7-12.3 0-17l48.1-48.1-48.1-48.1c-4.7-4.7-4.7-12.3 0-17l5.7-5.7c4.7-4.7 12.3-4.7 17 0l48.1 48.1 48.1-48.1c4.7-4.7 12.3-4.7 17 0l5.7 5.7c4.7 4.7 4.7 12.3 0 17L246.6 336l48.1 48.1z""></path></svg>    

            <span class=""text-gray-500 text-xs"">
                Wygasa <span class=""text-gray-700"">
                    <time datetime=""2025-10-05"">5 października 2025</time> <span class=""days-to-expire"">(za 8 dni)</span>
                </span>
            </span>
        </div>
    </div>

                                                                                <div class=""flex flex-row justify-center md:justify-end items-center mt-4 mb-2 sm:my-0 "">
            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1662244)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        type=""button""
        class=""inline-flex items-center btn btn-xs btn-outline-gray-light""
        @click.prevent=""toggleObservedState()""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg -ml-1 "" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    
 Obserwuj
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg -ml-1"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    
 Obserwujesz
        </span>
    </button>

        <button
            @click.prevent=""displayShareDialog('Udostępnij ofertę', 'Link do oferty', 'Oferta pracy')""
            type=""button""
            title=""Udostępnij ofertę""
            class=""inline-flex items-center btn btn-xs btn-outline-gray-light ml-2""
        >
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""share"" class=""svg-inline--fa fa-share fa-lg -ml-1 mr-1"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M564.907 196.35L388.91 12.366C364.216-13.45 320 3.746 320 40.016v88.154C154.548 130.155 0 160.103 0 331.19c0 94.98 55.84 150.231 89.13 174.571 24.233 17.722 58.021-4.992 49.68-34.51C100.937 336.887 165.575 321.972 320 320.16V408c0 36.239 44.19 53.494 68.91 27.65l175.998-184c14.79-15.47 14.79-39.83-.001-55.3zm-23.127 33.18l-176 184c-4.933 5.16-13.78 1.73-13.78-5.53V288c-171.396 0-295.313 9.707-243.98 191.7C72 453.36 32 405.59 32 331.19 32 171.18 194.886 160 352 160V40c0-7.262 8.851-10.69 13.78-5.53l176 184a7.978 7.978 0 0 1 0 11.06z""></path></svg>
    
 Udostępnij / drukuj
        </button>
    </div>

                                                                                                </div>
                                                            </div>

                                            <div>
                            
    
    
            <div class=""hidden xl:block apply-box border-t md:sticky bg-white w-full md:rounded-lg md:p-4 md:border md:border-gray-200 mt-4 md:mt-0 md:top-[5rem]"">
                            <div class=""text-center my-2 pb-2"">
                        
            
                            
        <button
            type=""button""
            class=""btn btn-brand btn-apply inline-flex items-center btn-block btn-xl""
            x-data=""{ clicked: false }""
            :disabled=""clicked""
                                @click=""if(!clicked) {
                        clicked = true;
                        externalApplyHandleClick('https://pl.mygigroup.com/members/jobs/seek/viewoffer/38765');
                    }""
                    >
            Aplikuj
        </button>
    
                </div>
                            
            
            
            <div class=""py-4 border-t mt-4"">
                

<div class=""flex items-center pr-4"">
    <div class=""mr-2 lg:mr-4 flex-shrink-0 self-center relative"" id=""js-btn-jobalert-1989903945"">
                    <span
                x-data=""jobAlertWidget""
                x-init=""initialize(null,         {
            criteria: {
                location: 'sosnowiec', profession: 'magazynier'
            },
            source: 'offer'
        }
    );""
                @click.prevent=""displayEmailSubscriptionPopup('/kandydat/logowanie?backUrl=https://www.aplikuj.pl/oferta/2960357/magazynier-5-570-zl-od-zaraz-umowa-o-prace-gi-group-s-a-%23cmd:job-alert-add')""
                class=""relative z-1 cursor-pointer""
            >
                        
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-3x rotate-12 text-primary"" style="""" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M207.1 16C207.1 7.164 215.2 0 223.1 0C232.8 0 240 7.164 240 16V32.79C320.9 40.82 384 109 384 192V221.1C384 264.8 401.4 306.7 432.3 337.7L435 340.4C443.3 348.7 448 359.1 448 371.7C448 396.2 428.2 416 403.7 416H44.28C19.83 416 0 396.2 0 371.7C0 359.1 4.666 348.7 12.97 340.4L15.72 337.7C46.63 306.7 64 264.8 64 221.1V192C64 109 127.1 40.82 208 32.79L207.1 16zM223.1 64C153.3 64 95.1 121.3 95.1 192V221.1C95.1 273.3 75.26 323.4 38.35 360.3L35.6 363C33.29 365.3 31.1 368.5 31.1 371.7C31.1 378.5 37.5 384 44.28 384H403.7C410.5 384 416 378.5 416 371.7C416 368.5 414.7 365.3 412.4 363L409.7 360.3C372.7 323.4 352 273.3 352 221.1V192C352 121.3 294.7 64 223.1 64H223.1zM223.1 480C237.9 480 249.8 471.1 254.2 458.7C257.1 450.3 266.3 445.1 274.6 448.9C282.9 451.9 287.3 461 284.4 469.3C275.6 494.2 251.9 512 223.1 512C196.1 512 172.4 494.2 163.6 469.3C160.7 461 165.1 451.9 173.4 448.9C181.7 445.1 190.9 450.3 193.8 458.7C198.2 471.1 210.1 480 223.1 480z""/></svg>    

            </span>
            </div>
    <div>
        <h4 class=""text-lg lg:text-xl font-bold whitespace-nowrap text-primary"">
            Praca alert - powiadomienia
                    </h4>
                    <p class=""font-medium "">Magazynier, Sosnowiec</p>
            </div>
</div>

<div class=""flex items-center justify-center sm:justify-end md:h-16"">
    <div
        x-data=""jobAlertWidget""
        x-cloak
        x-init=""initialize(0,         {
            criteria: {
                location: 'sosnowiec', profession: 'magazynier'
            },
            source: 'offer'
        }
    );""
        class=""pt-4 sm:pt-0 mt-0 sm:mt-4 w-full ""
    >
        <div class=""flex flex-col items-center"">
                    <button
                @click.prevent=""displayEmailSubscriptionPopup('/kandydat/logowanie?backUrl=https://www.aplikuj.pl/oferta/2960357/magazynier-5-570-zl-od-zaraz-umowa-o-prace-gi-group-s-a-%23cmd:job-alert-add')""
                class=""btn cursor-pointer justify-center  btn-primary""type=""button""
                role=""switch""
            >
                <span class=""px-2 whitespace-nowrap"">
                            
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512"" class=""svg-inline--fa  fa-1x mr-1text-white"" style=""""><path fill=""currentColor"" d=""M464 64H48C21.5 64 0 85.5 0 112v288c0 26.5 21.5 48 48 48h416c26.5 0 48-21.5 48-48V112c0-26.5-21.5-48-48-48zM48 96h416c8.8 0 16 7.2 16 16v41.4c-21.9 18.5-53.2 44-150.6 121.3-16.9 13.4-50.2 45.7-73.4 45.3-23.2.4-56.6-31.9-73.4-45.3C85.2 197.4 53.9 171.9 32 153.4V112c0-8.8 7.2-16 16-16zm416 320H48c-8.8 0-16-7.2-16-16V195c22.8 18.7 58.8 47.6 130.7 104.7 20.5 16.4 56.7 52.5 93.3 52.3 36.4.3 72.3-35.5 93.3-52.3 71.9-57.1 107.9-86 130.7-104.7v205c0 8.8-7.2 16-16 16z""/></svg>    
Włącz powiadomienia
                </span>
            </button>
        
                </div>
    </div>
</div>            </div>

                    <div class=""pt-4 mt-2 border-t"">
        <div class=""text-sm mb-2 font-medium"">Podobne oferty pracy</div>
        <ul role=""list"" class=""offer-list offer-list--condensed"">
                                            <li class=""offer-list__item"">
                    

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2973766/komisjoner-magazyn-z-warzywami-i-owocami-emstek-60km-od-bremy-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""offer-title gtm-offerdetails-offers-sidebar""
                title=""Komisjoner - Magazyn z warzywami i owocami - Emstek 60km od Bremy""
                
            >
                            Komisjoner - Magazyn z warzywami i owocami - Emstek 60km od Bremy
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2973766/komisjoner-magazyn-z-warzywami-i-owocami-emstek-60km-od-bremy-umowa-o-prace-sternjob-sp-z-o-o-"" class=""py-2 leading-4 gtm-offerdetails-offers-sidebar"">    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHOUHtO6BLVhic1Ig1vTCR8XBGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168916605665241800.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""STERNJOB SP. Z O.O.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">STERNJOB SP. Z O.O.</p>
                <ul class=""offer-card-labels-list"">
                    

                    
    


    
    
    








    



<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary"">
                    
                
<span class=""text-sm""><span class=""offer-salary text-xs font-medium "">            1 800 do 2 200 € <span class=""font-normal"">netto / mc</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Niemcy</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                </li>
                                            <li class=""offer-list__item"">
                    

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2969274/magazynier-komisjoner-okolice-hamburga-18-%E2%82%AC-brutto-bez-kwatery-umowa-o-prace-actuell-personal-gmbh""
                class=""offer-title gtm-offerdetails-offers-sidebar""
                title=""Magazynier/Komisjoner (okolice Hamburga) 18 € brutto bez kwatery""
                
            >
                            Magazynier/Komisjoner (okolice Hamburga) 18 € brutto bez kwatery
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2969274/magazynier-komisjoner-okolice-hamburga-18-%E2%82%AC-brutto-bez-kwatery-umowa-o-prace-actuell-personal-gmbh"" class=""py-2 leading-4 gtm-offerdetails-offers-sidebar"">    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbZyQNHOUHhNsxjUgyZkdV5sTiF5CBrW2nT8AyTK5L7TEwLUC1cSpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/25c3037abe5771e3c9ef3e0fece2bbb7.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""actuell Personal GmbH""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">actuell Personal GmbH</p>
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    



<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Niemcy</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                </li>
                                            <li class=""offer-list__item"">
                    

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979588/komisjoner-pracownik-magazynowy-warzywa-i-owoce-magazyny-lidla-niemcy-emstek-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""offer-title gtm-offerdetails-offers-sidebar""
                title=""Komisjoner/Pracownik Magazynowy (Warzywa i Owoce Magazyny Lidla) - Niemcy (Emstek)""
                
            >
                            Komisjoner/Pracownik Magazynowy (Warzywa i Owoce Magazyny Lidla) - Niemcy (Emstek)
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979588/komisjoner-pracownik-magazynowy-warzywa-i-owoce-magazyny-lidla-niemcy-emstek-umowa-o-prace-sternjob-sp-z-o-o-"" class=""py-2 leading-4 gtm-offerdetails-offers-sidebar"">    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHOUHtO6BLVhic1Ig1vTCR8XBGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168916605665241800.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""STERNJOB SP. Z O.O.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">STERNJOB SP. Z O.O.</p>
                <ul class=""offer-card-labels-list"">
                    

                    
    


    
    
    








    
    
    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary"">
                    
                
<span class=""text-sm""><span class=""offer-salary text-xs font-medium "">            2 000 do 2 300 € <span class=""font-normal"">netto / mc</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Niemcy</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                </li>
                                            <li class=""offer-list__item"">
                    

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2953304/pracownik-magazynu-sortowanie-paczek-dhl-bez-angielskiego-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""offer-title gtm-offerdetails-offers-sidebar""
                title=""Pracownik magazynu - Sortowanie paczek DHL - Bez angielskiego""
                
            >
                            Pracownik magazynu - Sortowanie paczek DHL - Bez angielskiego
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2953304/pracownik-magazynu-sortowanie-paczek-dhl-bez-angielskiego-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-"" class=""py-2 leading-4 gtm-offerdetails-offers-sidebar"">    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZwQNHJUHtP4hndgyY0IwxpTiV6XBCF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/172293714707321900.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Carriere Contracting International Recruitment Polska Sp. z o. o.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Carriere Contracting International Recruitment Polska Sp. z o. o.</p>
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    
    
    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Holandia</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                </li>
                    </ul>
    </div>

        </div>
    
                </div>
            
            

<div class=""flex justify-center"">
    <div
        x-cloak
        x-data=""shareUrlDialog""
        x-show=""isVisible""
        x-bind=""eventListeners""
        @keydown.escape.prevent.stop=""hideDialog""
        role=""dialog""
        aria-modal=""true""
        class=""fixed inset-0 overflow-y-auto""
    >
        <div x-show=""isVisible"" x-transition.opacity class=""modal-overlay"" aria-hidden=""true""></div>

        <div
            x-show=""isVisible"" x-transition
            @click=""hideDialog""
            class=""relative min-h-screen flex items-center justify-center p-4""
        >
            <div
                x-on:click.stop
                x-trap.noscroll.inert=""isVisible""
                class=""relative max-w-xl w-full bg-white rounded-lg shadow-lg p-4 overflow-y-auto""
            >
                <div class=""flex items-center justify-between"">
                    <div class=""flex-1 min-w-0"">
                        <h3 x-text=""dialogTitle""></h3>
                    </div>
                    <div class=""flex mt-0 ml-4"">
                        <button
                            type=""button""
                            class=""rounded-md text-gray-500 hover:text-gray-400 px-2 focus:outline-none focus:ring-0""
                            @click=""hideDialog""
                        >
                            <span class=""sr-only"">Zamknij</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times"" class=""svg-inline--fa fa-times fa-lg"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512""><path fill=""currentColor"" d=""M193.94 256L296.5 153.44l21.15-21.15c3.12-3.12 3.12-8.19 0-11.31l-22.63-22.63c-3.12-3.12-8.19-3.12-11.31 0L160 222.06 36.29 98.34c-3.12-3.12-8.19-3.12-11.31 0L2.34 120.97c-3.12 3.12-3.12 8.19 0 11.31L126.06 256 2.34 379.71c-3.12 3.12-3.12 8.19 0 11.31l22.63 22.63c3.12 3.12 8.19 3.12 11.31 0L160 289.94 262.56 392.5l21.15 21.15c3.12 3.12 8.19 3.12 11.31 0l22.63-22.63c3.12-3.12 3.12-8.19 0-11.31L193.94 256z""></path></svg>    

                        </button>
                    </div>
                </div>
                <div class=""py-3"">
                    <label for=""offer-url-input"" class=""block font-light text-gray-700 text-sm"" x-text=""shareLabel""></label>
                    <div class=""mt-1 block sm:flex flex-row items-center"">
                        <input
                            id=""offer-url-input""
                            type=""text""
                            readonly=""readonly""
                            value=""https://www.aplikuj.pl/oferta/2960357/magazynier-5-570-zl-od-zaraz-umowa-o-prace-gi-group-s-a-""
                            class=""grow w-full rounded-md border-gray-300 shadow-sm sm:text-sm focus:outline-none focus:ring-0 focus:ring-offset-0""
                        />
                        <div class=""px-2 py-2 sm:py-0 text-center"" x-show=""isClipboardAvailable"">
                            <button
                                type=""button""
                                class=""inline-flex items-center btn btn-outline-gray h-8 mx-auto whitespace-nowrap""
                                @click=""copyLinkToClipboard;""
                            >
                                        
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512"" class=""svg-inline--fa fa-lg -ml-1 mr-2"" style=""""><path fill=""currentColor"" d=""M320 64h-49.61C262.1 27.48 230.7 0 192 0S121 27.48 113.6 64H64C28.65 64 0 92.66 0 128v320c0 35.34 28.65 64 64 64h256c35.35 0 64-28.66 64-64V128C384 92.66 355.3 64 320 64zM192 48c13.23 0 24 10.77 24 24S205.2 96 192 96S168 85.23 168 72S178.8 48 192 48zM336 448c0 8.82-7.178 16-16 16H64c-8.822 0-16-7.18-16-16V128c0-8.82 7.178-16 16-16h18.26C80.93 117.1 80 122.4 80 128v16C80 152.8 87.16 160 96 160h192c8.836 0 16-7.164 16-16V128c0-5.559-.9316-10.86-2.264-16H320c8.822 0 16 7.18 16 16V448z""/></svg>    

                                Skopiuj link
                            </button>
                        </div>
                    </div>
                </div>
                <a class=""inline-flex items-center btn btn-outline-gray h-8 mx-auto whitespace-nowrap"" href=""/oferta/2960357/pdf"" rel=""nofollow"">
                            
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512"" class=""svg-inline--fa  fa-lg -ml-1 mr-2"" style=""""><path fill=""currentColor"" d=""M245.4 379.1C248.4 382.7 252.2 384 256 384s7.594-1.344 10.62-4.047l144-128c6.594-5.859 7.219-15.98 1.344-22.58c-5.875-6.625-16.06-7.234-22.59-1.328L272 332.4V16C272 7.156 264.8 0 256 0S240 7.156 240 16v316.4L122.6 228C116.1 222.1 105.9 222.8 100 229.4C94.16 235.1 94.78 246.1 101.4 251.1L245.4 379.1zM448 320h-48c-8.836 0-16 7.162-16 16c0 8.836 7.164 16 16 16H448c17.67 0 32 14.33 32 32v64c0 17.67-14.33 32-32 32H64c-17.67 0-32-14.33-32-32v-64c0-17.67 14.33-32 32-32h48C120.8 352 128 344.8 128 336C128 327.2 120.8 320 112 320H64c-35.35 0-64 28.65-64 64v64c0 35.35 28.65 64 64 64h384c35.35 0 64-28.65 64-64v-64C512 348.7 483.3 320 448 320zM440 416c0-13.25-10.75-24-24-24s-24 10.75-24 24s10.75 24 24 24S440 429.3 440 416z""/></svg>
    

                    Pobierz jako PDF
                </a>
                <div class=""py-3"">
                    <label for=""offer-url"" class=""block font-light text-gray-700 text-sm"">Podziel się</label>
                    <div class=""mt-1 pt-1"">
                        <div class=""sharethis-inline-share-buttons""></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src='https://platform-api.sharethis.com/js/sharethis.js#property=647efd6da660b80019d5605b&product=inline-share-buttons' defer></script>
            </div>
                </div>

            </div>
</section>

                        
<div class=""w-full"" style=""background-color: #006083"">
    <div class=""py-16 container mx-auto max-w-4xl flex justify-center"">
        <div class=""flex flex-col items-center space-y-4 md:space-y-8"">
            <div class=""card flex-shrink-0"">
                <div class=""card-content flex justify-center"">
                        
                    <img
            src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtmisYuldmqYUeppGKqLwlzC0BdBiCEzIlpHmeMFHpzFCkz2Q0GhToNyDNtGOrwNWLrtsp4Poo3WZnRcDzrTWYWkMsBwnnklpP41ZfngpLa-m87faAjHUWtlbb3TF02kc6woW_dzCGoXtcqYaC9yeLbWBpiZZVAZGaHUqTwIvLycmeKuY0aL7WVKQslVQk61dIvu4cnZfCxCmES2zNCL6rP4ei8-pN4ok3s9LaEvdpzsDe6cg7gB2qwpRofUmtFjfEIwNO_N3m8HDl1msXBR1AJBinIYw8tyYeqrMEI332vVpaABec76wEn3mgvRr0Nyo5ZNbyBi2xTL5I27V2aVL6Uewatg/952f31ec324e2b01e08bf66a95797441.webp""
            width=""200""
            height=""200""
            class=""inline-block md:mr-4 w-32""
            alt=""Gi Group S.A.""
        >
    
                </div>
            </div>
            <div class=""text-center"">
                <h4 class=""text-xl md:text-4xl text-white font-bold"">Dowiedz się więcej o pracodawcy</h4>
            </div>
            <div class=""text-center"">
                <h4 class=""text-lg md:text-2xl text-white"">Gi Group S.A.</h4>
            </div>

            <a
                href=""/pracodawca/10423/gi-group-s-a-""
                class=""btn btn-lg whitespace-nowrap cursor-pointer justify-center hover:brightness-90 hover:text-white text-white""
                style=""background-color: #00B2C5; border-color: #00B2C5;""
            >
                Zobacz profil pracodawcy
                        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""angle-right"" class=""svg-inline--fa fa-angle-right fa-lg text-primary ml-2 text-white"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 192 512""><path fill=""currentColor"" d=""M166.9 264.5l-117.8 116c-4.7 4.7-12.3 4.7-17 0l-7.1-7.1c-4.7-4.7-4.7-12.3 0-17L127.3 256 25.1 155.6c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0l117.8 116c4.6 4.7 4.6 12.3-.1 17z""></path></svg>
    

            </a>
        </div>
    </div>
</div>

            


                    
        <section class=""section section--condensed bg-gray-50"">
        <div class=""container max-w-7xl mx-auto"" x-data=""{ loaderOffers: true}"">
            <div class=""py-4"">
                        <div class=""relative"">
        <div class=""absolute inset-0 flex items-center"" aria-hidden=""true"">
            <div class=""w-full border-t border-gray-300""></div>
        </div>
        <div class=""relative flex justify-start"">
            <span class=""pr-3 bg-gray-50 text-lg font-medium text-gray-900"">
              <h2>Pozostałe oferty pracodawcy</h2>
            </span>
        </div>
    </div>

            </div>
            <div class=""h-50"">
                <div x-show=""loaderOffers"" class=""loader-overlay"">
    <div class=""loader ease-linear rounded-full border-4 border-t-4 border-gray-200 border-t-primary h-12 w-12 mb-4""></div>
</div>

                <div x-cloak class=""offer-list"" x-init=""loaderOffers = false"">
                                        <div class=""h-50"">
                                <div class=""swiper-container"">
        <div
            x-cloak
            x-data=""slider()""
            x-init=""sliderInit('recommended-slider', 4, 0)""
            class=""swiper recommended-slider""
        >
            <div class=""swiper-wrapper"">
                                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979655/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator maszyn produkcyjnych - Technik""
                
            >
                            Operator maszyn produkcyjnych - Technik
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979655/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Sady</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979646/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator maszyn produkcyjnych - Technik""
                
            >
                            Operator maszyn produkcyjnych - Technik
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979646/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Cerekwica</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979642/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator maszyn produkcyjnych - Technik""
                
            >
                            Operator maszyn produkcyjnych - Technik
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979642/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Mrowino</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979639/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator maszyn produkcyjnych - Technik""
                
            >
                            Operator maszyn produkcyjnych - Technik
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979639/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Kaźmierz</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979637/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator maszyn produkcyjnych - Technik""
                
            >
                            Operator maszyn produkcyjnych - Technik
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979637/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Tarnowo Podgórne</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2979635/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator maszyn produkcyjnych - Technik""
                
            >
                            Operator maszyn produkcyjnych - Technik
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2979635/operator-maszyn-produkcyjnych-technik-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Jankowice</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2952139/operator-produkcji-|-umowa-o-prace-|-komfortowe-zakwaterowanie-|-praca-od-zaraz-umowa-o-prace-tymczasowa-gi-group-s-a-""
                class=""offer-title ""
                title=""Operator Produkcji | Umowa o pracę | Komfortowe zakwaterowanie | Praca od zaraz""
                
            >
                            Operator Produkcji | Umowa o pracę | Komfortowe zakwaterowanie | Praca od zaraz
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2952139/operator-produkcji-|-umowa-o-prace-|-komfortowe-zakwaterowanie-|-praca-od-zaraz-umowa-o-prace-tymczasowa-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

                
    


    
    
    
    

    
    





    

    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary"">
                    
                
<span class=""text-sm""><span class=""offer-salary text-xs font-medium "">            4 666 do 5 700  zł <span class=""offer-salary__period"">/ mc</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Dzierżoniów</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2976089/pracownik-magazynu-od-zaraz-5-350-zl-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Pracownik magazynu // od zaraz // 5.350 zł""
                
            >
                            Pracownik magazynu // od zaraz // 5.350 zł
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2976089/pracownik-magazynu-od-zaraz-5-350-zl-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    
    
    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Poznań</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2969234/pracownik-magazynowy-umowa-o-prace-tymczasowa-gi-group-s-a-""
                class=""offer-title ""
                title=""Pracownik magazynowy""
                
            >
                            Pracownik magazynowy
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2969234/pracownik-magazynowy-umowa-o-prace-tymczasowa-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    











<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Sosnowiec</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2976083/samodzielny-ksiegowy-a-z-j-angielskim-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Samodzielny Księgowy/-a z j. angielskim""
                
            >
                            Samodzielny Księgowy/-a z j. angielskim
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2976083/samodzielny-ksiegowy-a-z-j-angielskim-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Ustroń</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2977196/pracownik-dostaw-jedzenia-praca-dodatkowa-umowa-zlecenie-gi-group-s-a-""
                class=""offer-title ""
                title=""Pracownik dostaw (jedzenia) // praca dodatkowa""
                
            >
                            Pracownik dostaw (jedzenia) // praca dodatkowa
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2977196/pracownik-dostaw-jedzenia-praca-dodatkowa-umowa-zlecenie-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

                
    


    
    
    
    

    
    




    
    
    
    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary"">
                    
                
<span class=""text-sm""><span class=""offer-salary text-xs font-medium "">            31 do 31  zł <span class=""offer-salary__period"">/ h</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Warszawa</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2976086/samodzielny-ksiegowy-a-z-j-angielskim-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Samodzielny Księgowy/-a z j. angielskim""
                
            >
                            Samodzielny Księgowy/-a z j. angielskim
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2976086/samodzielny-ksiegowy-a-z-j-angielskim-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Strumień</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2969201/pracownik-magazynowy-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Pracownik magazynowy""
                
            >
                            Pracownik magazynowy
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2969201/pracownik-magazynowy-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    











<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Poznań</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2978433/slusarz-narzedziowy|-premiebenefity-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Ślusarz narzędziowy| premie+benefity""
                
            >
                            Ślusarz narzędziowy| premie+benefity
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2978433/slusarz-narzedziowy|-premiebenefity-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    

    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Strzegom</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2969240/pracownik-produkcji-bez-doswiadczenia-od-zaraz-umowa-o-prace-tymczasowa-gi-group-s-a-""
                class=""offer-title ""
                title=""Pracownik produkcji bez doświadczenia!- od zaraz""
                
            >
                            Pracownik produkcji bez doświadczenia!- od zaraz
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2969240/pracownik-produkcji-bez-doswiadczenia-od-zaraz-umowa-o-prace-tymczasowa-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    









    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Olkusz</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2976136/spawacz-tig-wroclaw-%E2%80%93-wolne-weekendy-6500%E2%80%938000-pln-benefity-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Spawacz TIG Wrocław – wolne weekendy, 6500–8000 PLN + benefity!""
                
            >
                            Spawacz TIG Wrocław – wolne weekendy, 6500–8000 PLN + benefity!
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2976136/spawacz-tig-wroclaw-%E2%80%93-wolne-weekendy-6500%E2%80%938000-pln-benefity-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

                
    


    
    
    
    

    
    





    

    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary"">
                    
                
<span class=""text-sm""><span class=""offer-salary text-xs font-medium "">            6 500 do 8 000  zł <span class=""offer-salary__period"">/ mc</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Wrocław</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2978412/slusarz-narzedziowy|-premiebenefity-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Ślusarz narzędziowy| premie+benefity""
                
            >
                            Ślusarz narzędziowy| premie+benefity
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2978412/slusarz-narzedziowy|-premiebenefity-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    

    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Świdnica</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2978420/slusarz-narzedziowy|-premiebenefity-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Ślusarz narzędziowy| premie+benefity""
                
            >
                            Ślusarz narzędziowy| premie+benefity
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2978420/slusarz-narzedziowy|-premiebenefity-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    

    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Świebodzice</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2976084/samodzielny-ksiegowy-a-z-j-angielskim-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Samodzielny Księgowy/-a z j. angielskim""
                
            >
                            Samodzielny Księgowy/-a z j. angielskim
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2976084/samodzielny-ksiegowy-a-z-j-angielskim-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Zebrzydowice</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>                            <div class=""swiper-slide"">
                                

<div class=""offer-card small !border-0"">
    <div class=""offer-card-content"">
        


    

<div class=""offer-card-small-wrapper"">


    <div>
        <h3 class=""offer-header"">
            <a
                href=""/oferta/2976113/pracownik-magazynu-od-zaraz-5-350-zl-umowa-o-prace-gi-group-s-a-""
                class=""offer-title ""
                title=""Pracownik magazynu // od zaraz // 5.350 zł""
                
            >
                            Pracownik magazynu // od zaraz // 5.350 zł
    
            </a>
        </h3>
        <div class=""flex"">
            <div class=""offer-company-logo-wrapper"">
                <a href=""/oferta/2976113/pracownik-magazynu-od-zaraz-5-350-zl-umowa-o-prace-gi-group-s-a-"" class=""py-2 leading-4 "">    
                                    <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Gi Group S.A.""
            >
            </a>
            </div>
            <div class=""text-sm offer-description"">
                <p class=""offer-company-name"">Gi Group S.A.</p>
                <ul class=""offer-card-labels-list"">
                    

    



    
    
    
    

    
    





    
    
    
<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    
<span class=""text-sm"">Września</span>
        </li>
                </ul>
            </div>
        </div>
    </div>

    <div class=""offer-extra-info text-xs flex flex-row flex-nowrap justify-between items-center"">
        <div>Dodana <time>26 wrz 2025</time></div>

    </div>
</div>



    </div>
</div>
                            </div>
            </div>
        </div>

                    <div class=""swiper-pagination js-swiper-pagination-recommended-slider !-bottom-2""></div>
        
                    <div class=""swiper-button-prev js-swiper-prev-recommended-slider hidden sm:flex swiper-button-disabled""></div>
            <div class=""swiper-button-next js-swiper-next-recommended-slider hidden sm:flex swiper-button-disabled""></div>
            </div>

                    </div>
                </div>
            </div>
        </div>
    </section>

    
                    
    <section class=""section section--condensed bg-gray-100"">
        <div class=""container max-w-7xl mx-auto"">
            <div class=""py-4"">
                        <div class=""relative"">
        <div class=""absolute inset-0 flex items-center"" aria-hidden=""true"">
            <div class=""w-full border-t border-gray-300""></div>
        </div>
        <div class=""relative flex justify-start"">
            <span class=""pr-3 bg-gray-100 text-lg font-medium text-gray-900"">
              <h2 id=""similar-section"">Podobne oferty pracy</h2>
            </span>
        </div>
    </div>

            </div>
            <ul role=""list"" class=""mt-4 offer-list"">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1675654""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2973766/komisjoner-magazyn-z-warzywami-i-owocami-emstek-60km-od-bremy-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHOUHtO6BLVhic1Ig1vTCR8XBGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168916605665241800.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""STERNJOB SP. Z O.O.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2973766/komisjoner-magazyn-z-warzywami-i-owocami-emstek-60km-od-bremy-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""offer-title""
                title=""Komisjoner - Magazyn z warzywami i owocami - Emstek 60km od Bremy""
                
            >
                            Komisjoner - Magazyn z warzywami i owocami - Emstek 60km od Bremy
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    STERNJOB SP. Z O.O.
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

                    
    


    
    
    








    



<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary "">
                    
                

            <span class=""text-sm""><span class=""offer-salary font-bold "">            1 800 do 2 200 € <span class=""font-normal"">netto / mies.</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Niemcy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 46.7 42"" class=""svg-inline--fa fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M44.2,15.6H32.1c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h12.1c0.2,0,0.4-0.2,0.4-0.4V16 C44.6,15.8,44.4,15.6,44.2,15.6z M37.9,22h-5.8c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h5.8c0.2,0,0.4-0.2,0.4-0.4v-2.3 C38.3,22.2,38.1,22,37.9,22z M22.1,11.4h-2c-0.3,0-0.5,0.2-0.5,0.5v11.7c0,0.2,0.1,0.3,0.2,0.4l7,5.1c0.2,0.2,0.6,0.1,0.7-0.1 l1.2-1.7v0c0.2-0.2,0.1-0.6-0.1-0.7l-6-4.3V11.9C22.6,11.6,22.4,11.4,22.1,11.4L22.1,11.4z""/><path fill=""currentColor"" d=""M37.6,28h-2.7c-0.3,0-0.5,0.1-0.7,0.4c-0.6,1-1.3,1.8-2.1,2.6c-1.4,1.4-3,2.5-4.8,3.2c-1.9,0.8-3.8,1.2-5.9,1.2 c-2,0-4-0.4-5.9-1.2c-1.8-0.8-3.4-1.8-4.8-3.2s-2.5-3-3.2-4.8c-0.8-1.9-1.2-3.8-1.2-5.9c0-2,0.4-4,1.2-5.9c0.8-1.8,1.8-3.4,3.2-4.8 s3-2.5,4.8-3.2c1.9-0.8,3.8-1.2,5.9-1.2c2,0,4,0.4,5.9,1.2c1.8,0.8,3.4,1.8,4.8,3.2c0.8,0.8,1.5,1.7,2.1,2.6 c0.1,0.2,0.4,0.4,0.7,0.4h2.7c0.3,0,0.5-0.3,0.4-0.6C34.9,5.9,28.6,1.9,21.7,1.8C11.4,1.7,3,10.1,2.9,20.3 c0,10.2,8.3,18.5,18.5,18.5c7.1,0,13.4-4,16.5-10.2C38.1,28.3,37.9,28,37.6,28L37.6,28z""/></svg>    

                            Aplikuj szybko
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
            <span class=""offer-badge badge-recommended"">Polecana</span>
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>26 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1675654)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1671161""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2969274/magazynier-komisjoner-okolice-hamburga-18-%E2%82%AC-brutto-bez-kwatery-umowa-o-prace-actuell-personal-gmbh""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbZyQNHOUHhNsxjUgyZkdV5sTiF5CBrW2nT8AyTK5L7TEwLUC1cSpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/25c3037abe5771e3c9ef3e0fece2bbb7.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""actuell Personal GmbH""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2969274/magazynier-komisjoner-okolice-hamburga-18-%E2%82%AC-brutto-bez-kwatery-umowa-o-prace-actuell-personal-gmbh""
                class=""offer-title""
                title=""Magazynier/Komisjoner (okolice Hamburga) 18 € brutto bez kwatery""
                
            >
                            Magazynier/Komisjoner (okolice Hamburga) 18 € brutto bez kwatery
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    actuell Personal GmbH
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    



<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Niemcy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 46.7 42"" class=""svg-inline--fa fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M44.2,15.6H32.1c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h12.1c0.2,0,0.4-0.2,0.4-0.4V16 C44.6,15.8,44.4,15.6,44.2,15.6z M37.9,22h-5.8c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h5.8c0.2,0,0.4-0.2,0.4-0.4v-2.3 C38.3,22.2,38.1,22,37.9,22z M22.1,11.4h-2c-0.3,0-0.5,0.2-0.5,0.5v11.7c0,0.2,0.1,0.3,0.2,0.4l7,5.1c0.2,0.2,0.6,0.1,0.7-0.1 l1.2-1.7v0c0.2-0.2,0.1-0.6-0.1-0.7l-6-4.3V11.9C22.6,11.6,22.4,11.4,22.1,11.4L22.1,11.4z""/><path fill=""currentColor"" d=""M37.6,28h-2.7c-0.3,0-0.5,0.1-0.7,0.4c-0.6,1-1.3,1.8-2.1,2.6c-1.4,1.4-3,2.5-4.8,3.2c-1.9,0.8-3.8,1.2-5.9,1.2 c-2,0-4-0.4-5.9-1.2c-1.8-0.8-3.4-1.8-4.8-3.2s-2.5-3-3.2-4.8c-0.8-1.9-1.2-3.8-1.2-5.9c0-2,0.4-4,1.2-5.9c0.8-1.8,1.8-3.4,3.2-4.8 s3-2.5,4.8-3.2c1.9-0.8,3.8-1.2,5.9-1.2c2,0,4,0.4,5.9,1.2c1.8,0.8,3.4,1.8,4.8,3.2c0.8,0.8,1.5,1.7,2.1,2.6 c0.1,0.2,0.4,0.4,0.7,0.4h2.7c0.3,0,0.5-0.3,0.4-0.6C34.9,5.9,28.6,1.9,21.7,1.8C11.4,1.7,3,10.1,2.9,20.3 c0,10.2,8.3,18.5,18.5,18.5c7.1,0,13.4-4,16.5-10.2C38.1,28.3,37.9,28,37.6,28L37.6,28z""/></svg>    

                            Aplikuj szybko
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>26 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1671161)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1681477""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2979588/komisjoner-pracownik-magazynowy-warzywa-i-owoce-magazyny-lidla-niemcy-emstek-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHOUHtO6BLVhic1Ig1vTCR8XBGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168916605665241800.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""STERNJOB SP. Z O.O.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2979588/komisjoner-pracownik-magazynowy-warzywa-i-owoce-magazyny-lidla-niemcy-emstek-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""offer-title""
                title=""Komisjoner/Pracownik Magazynowy (Warzywa i Owoce Magazyny Lidla) - Niemcy (Emstek)""
                
            >
                            Komisjoner/Pracownik Magazynowy (Warzywa i Owoce Magazyny Lidla) - Niemcy (Emstek)
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    STERNJOB SP. Z O.O.
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

                    
    


    
    
    








    
    
    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary "">
                    
                

            <span class=""text-sm""><span class=""offer-salary font-bold "">            2 000 do 2 300 € <span class=""font-normal"">netto / mies.</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Niemcy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--inexperience "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M638.9 209.7l-8-13.9c-2.2-3.8-7.1-5.1-10.9-2.9l-108 63V240c0-26.5-21.5-48-48-48H320v62.2c0 16-10.9 30.8-26.6 33.3-20 3.3-37.4-12.2-37.4-31.6v-94.3c0-13.8 7.1-26.6 18.8-33.9l33.4-20.9c11.4-7.1 24.6-10.9 38.1-10.9h103.2l118.5-67c3.8-2.2 5.2-7.1 3-10.9l-8-14c-2.2-3.8-7.1-5.2-10.9-3l-111 63h-94.7c-19.5 0-38.6 5.5-55.1 15.8l-33.5 20.9c-17.5 11-28.7 28.6-32.2 48.5l-62.5 37c-21.6 13-35.1 36.7-35.1 61.9v38.6L4 357.1c-3.8 2.2-5.2 7.1-3 10.9l8 13.9c2.2 3.8 7.1 5.2 10.9 3L160 305.3v-57.2c0-14 7.5-27.2 19.4-34.4l44.6-26.4v65.9c0 33.4 24.3 63.3 57.6 66.5 38.1 3.7 70.4-26.3 70.4-63.7v-32h112c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16h-24v36c0 19.8-16 35.8-35.8 35.8h-16.1v16c0 22.2-18 40.2-40.2 40.2H238.5l-115.6 67.3c-3.8 2.2-5.1 7.1-2.9 10.9l8 13.8c2.2 3.8 7.1 5.1 10.9 2.9L247.1 448h100.8c34.8 0 64-24.8 70.8-57.7 30.4-6.7 53.3-33.9 53.3-66.3v-4.7c13.6-2.3 24.6-10.6 31.8-21.7l132.2-77c3.8-2.2 5.1-7.1 2.9-10.9z""/></svg>    

            <span class=""text-sm"">bez doświadczenia</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 46.7 42"" class=""svg-inline--fa fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M44.2,15.6H32.1c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h12.1c0.2,0,0.4-0.2,0.4-0.4V16 C44.6,15.8,44.4,15.6,44.2,15.6z M37.9,22h-5.8c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h5.8c0.2,0,0.4-0.2,0.4-0.4v-2.3 C38.3,22.2,38.1,22,37.9,22z M22.1,11.4h-2c-0.3,0-0.5,0.2-0.5,0.5v11.7c0,0.2,0.1,0.3,0.2,0.4l7,5.1c0.2,0.2,0.6,0.1,0.7-0.1 l1.2-1.7v0c0.2-0.2,0.1-0.6-0.1-0.7l-6-4.3V11.9C22.6,11.6,22.4,11.4,22.1,11.4L22.1,11.4z""/><path fill=""currentColor"" d=""M37.6,28h-2.7c-0.3,0-0.5,0.1-0.7,0.4c-0.6,1-1.3,1.8-2.1,2.6c-1.4,1.4-3,2.5-4.8,3.2c-1.9,0.8-3.8,1.2-5.9,1.2 c-2,0-4-0.4-5.9-1.2c-1.8-0.8-3.4-1.8-4.8-3.2s-2.5-3-3.2-4.8c-0.8-1.9-1.2-3.8-1.2-5.9c0-2,0.4-4,1.2-5.9c0.8-1.8,1.8-3.4,3.2-4.8 s3-2.5,4.8-3.2c1.9-0.8,3.8-1.2,5.9-1.2c2,0,4,0.4,5.9,1.2c1.8,0.8,3.4,1.8,4.8,3.2c0.8,0.8,1.5,1.7,2.1,2.6 c0.1,0.2,0.4,0.4,0.7,0.4h2.7c0.3,0,0.5-0.3,0.4-0.6C34.9,5.9,28.6,1.9,21.7,1.8C11.4,1.7,3,10.1,2.9,20.3 c0,10.2,8.3,18.5,18.5,18.5c7.1,0,13.4-4,16.5-10.2C38.1,28.3,37.9,28,37.6,28L37.6,28z""/></svg>    

                            Aplikuj szybko
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
            <span class=""offer-badge badge-recommended"">Polecana</span>
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>26 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1681477)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1655190""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2953304/pracownik-magazynu-sortowanie-paczek-dhl-bez-angielskiego-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZwQNHJUHtP4hndgyY0IwxpTiV6XBCF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/172293714707321900.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Carriere Contracting International Recruitment Polska Sp. z o. o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2953304/pracownik-magazynu-sortowanie-paczek-dhl-bez-angielskiego-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""offer-title""
                title=""Pracownik magazynu - Sortowanie paczek DHL - Bez angielskiego""
                
            >
                            Pracownik magazynu - Sortowanie paczek DHL - Bez angielskiego
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Carriere Contracting International Recruitment Pol...
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    
    
    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Holandia</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę tymczasową</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--inexperience "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M638.9 209.7l-8-13.9c-2.2-3.8-7.1-5.1-10.9-2.9l-108 63V240c0-26.5-21.5-48-48-48H320v62.2c0 16-10.9 30.8-26.6 33.3-20 3.3-37.4-12.2-37.4-31.6v-94.3c0-13.8 7.1-26.6 18.8-33.9l33.4-20.9c11.4-7.1 24.6-10.9 38.1-10.9h103.2l118.5-67c3.8-2.2 5.2-7.1 3-10.9l-8-14c-2.2-3.8-7.1-5.2-10.9-3l-111 63h-94.7c-19.5 0-38.6 5.5-55.1 15.8l-33.5 20.9c-17.5 11-28.7 28.6-32.2 48.5l-62.5 37c-21.6 13-35.1 36.7-35.1 61.9v38.6L4 357.1c-3.8 2.2-5.2 7.1-3 10.9l8 13.9c2.2 3.8 7.1 5.2 10.9 3L160 305.3v-57.2c0-14 7.5-27.2 19.4-34.4l44.6-26.4v65.9c0 33.4 24.3 63.3 57.6 66.5 38.1 3.7 70.4-26.3 70.4-63.7v-32h112c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16h-24v36c0 19.8-16 35.8-35.8 35.8h-16.1v16c0 22.2-18 40.2-40.2 40.2H238.5l-115.6 67.3c-3.8 2.2-5.1 7.1-2.9 10.9l8 13.8c2.2 3.8 7.1 5.1 10.9 2.9L247.1 448h100.8c34.8 0 64-24.8 70.8-57.7 30.4-6.7 53.3-33.9 53.3-66.3v-4.7c13.6-2.3 24.6-10.6 31.8-21.7l132.2-77c3.8-2.2 5.1-7.1 2.9-10.9z""/></svg>    

            <span class=""text-sm"">bez doświadczenia</span>
        </li>
                </ul>

                                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span class=""offer-card-date font-medium"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512"" class=""svg-inline--fa  fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M240 320C240 328.8 232.8 336 223.1 336C215.2 336 207.1 328.8 207.1 320V208C207.1 199.2 215.2 192 223.1 192C232.8 192 240 199.2 240 208V320zM127.1 16C127.1 7.164 135.2 0 143.1 0H304C312.8 0 320 7.164 320 16C320 24.84 312.8 32 304 32H240V96.61C289.4 100.4 333.1 121.4 367.7 153.6L404.7 116.7C410.9 110.4 421.1 110.4 427.3 116.7C433.6 122.9 433.6 133.1 427.3 139.3L389.1 177.5C416 212.6 432 256.4 432 304C432 418.9 338.9 512 224 512C109.1 512 16 418.9 16 304C16 194.5 100.6 104.8 208 96.61V32H144C135.2 32 128 24.84 128 16H127.1zM223.1 480C321.2 480 400 401.2 400 304C400 206.8 321.2 128 223.1 128C126.8 128 47.1 206.8 47.1 304C47.1 401.2 126.8 480 223.1 480z""/></svg>
    

                            Wygasa <time>za 2 dni</time>
                        </span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1655190)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1655192""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2953306/order-picker-ubrania-znanych-marek-1585-eur-h-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZwQNHJUHtP4hndgyY0IwxpTiV6XBCF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/172293714707321900.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Carriere Contracting International Recruitment Polska Sp. z o. o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2953306/order-picker-ubrania-znanych-marek-1585-eur-h-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""offer-title""
                title=""Order Picker - Ubrania znanych marek 15,85 EUR/h""
                
            >
                            Order Picker - Ubrania znanych marek 15,85 EUR/h
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Carriere Contracting International Recruitment Pol...
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    
    
    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Holandia</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę tymczasową</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--inexperience "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M638.9 209.7l-8-13.9c-2.2-3.8-7.1-5.1-10.9-2.9l-108 63V240c0-26.5-21.5-48-48-48H320v62.2c0 16-10.9 30.8-26.6 33.3-20 3.3-37.4-12.2-37.4-31.6v-94.3c0-13.8 7.1-26.6 18.8-33.9l33.4-20.9c11.4-7.1 24.6-10.9 38.1-10.9h103.2l118.5-67c3.8-2.2 5.2-7.1 3-10.9l-8-14c-2.2-3.8-7.1-5.2-10.9-3l-111 63h-94.7c-19.5 0-38.6 5.5-55.1 15.8l-33.5 20.9c-17.5 11-28.7 28.6-32.2 48.5l-62.5 37c-21.6 13-35.1 36.7-35.1 61.9v38.6L4 357.1c-3.8 2.2-5.2 7.1-3 10.9l8 13.9c2.2 3.8 7.1 5.2 10.9 3L160 305.3v-57.2c0-14 7.5-27.2 19.4-34.4l44.6-26.4v65.9c0 33.4 24.3 63.3 57.6 66.5 38.1 3.7 70.4-26.3 70.4-63.7v-32h112c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16h-24v36c0 19.8-16 35.8-35.8 35.8h-16.1v16c0 22.2-18 40.2-40.2 40.2H238.5l-115.6 67.3c-3.8 2.2-5.1 7.1-2.9 10.9l8 13.8c2.2 3.8 7.1 5.1 10.9 2.9L247.1 448h100.8c34.8 0 64-24.8 70.8-57.7 30.4-6.7 53.3-33.9 53.3-66.3v-4.7c13.6-2.3 24.6-10.6 31.8-21.7l132.2-77c3.8-2.2 5.1-7.1 2.9-10.9z""/></svg>    

            <span class=""text-sm"">bez doświadczenia</span>
        </li>
                </ul>

                                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span class=""offer-card-date font-medium"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512"" class=""svg-inline--fa  fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M240 320C240 328.8 232.8 336 223.1 336C215.2 336 207.1 328.8 207.1 320V208C207.1 199.2 215.2 192 223.1 192C232.8 192 240 199.2 240 208V320zM127.1 16C127.1 7.164 135.2 0 143.1 0H304C312.8 0 320 7.164 320 16C320 24.84 312.8 32 304 32H240V96.61C289.4 100.4 333.1 121.4 367.7 153.6L404.7 116.7C410.9 110.4 421.1 110.4 427.3 116.7C433.6 122.9 433.6 133.1 427.3 139.3L389.1 177.5C416 212.6 432 256.4 432 304C432 418.9 338.9 512 224 512C109.1 512 16 418.9 16 304C16 194.5 100.6 104.8 208 96.61V32H144C135.2 32 128 24.84 128 16H127.1zM223.1 480C321.2 480 400 401.2 400 304C400 206.8 321.2 128 223.1 128C126.8 128 47.1 206.8 47.1 304C47.1 401.2 126.8 480 223.1 480z""/></svg>
    

                            Wygasa <time>za 2 dni</time>
                        </span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1655192)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                      



<li
    x-ref=""offer-1680319""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2978430/magazynier-umowa-o-prace-aluform-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbV4QNHOUHtN5RjRgSc1JQ9vTSN5WxyF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/155351602464516500.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Aluform Sp. z o.o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2978430/magazynier-umowa-o-prace-aluform-sp-z-o-o-""
                class=""offer-title""
                title=""Magazynier""
                
            >
                            Magazynier
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Aluform Sp. z o.o.
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    









    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Tychy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 46.7 42"" class=""svg-inline--fa fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M44.2,15.6H32.1c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h12.1c0.2,0,0.4-0.2,0.4-0.4V16 C44.6,15.8,44.4,15.6,44.2,15.6z M37.9,22h-5.8c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h5.8c0.2,0,0.4-0.2,0.4-0.4v-2.3 C38.3,22.2,38.1,22,37.9,22z M22.1,11.4h-2c-0.3,0-0.5,0.2-0.5,0.5v11.7c0,0.2,0.1,0.3,0.2,0.4l7,5.1c0.2,0.2,0.6,0.1,0.7-0.1 l1.2-1.7v0c0.2-0.2,0.1-0.6-0.1-0.7l-6-4.3V11.9C22.6,11.6,22.4,11.4,22.1,11.4L22.1,11.4z""/><path fill=""currentColor"" d=""M37.6,28h-2.7c-0.3,0-0.5,0.1-0.7,0.4c-0.6,1-1.3,1.8-2.1,2.6c-1.4,1.4-3,2.5-4.8,3.2c-1.9,0.8-3.8,1.2-5.9,1.2 c-2,0-4-0.4-5.9-1.2c-1.8-0.8-3.4-1.8-4.8-3.2s-2.5-3-3.2-4.8c-0.8-1.9-1.2-3.8-1.2-5.9c0-2,0.4-4,1.2-5.9c0.8-1.8,1.8-3.4,3.2-4.8 s3-2.5,4.8-3.2c1.9-0.8,3.8-1.2,5.9-1.2c2,0,4,0.4,5.9,1.2c1.8,0.8,3.4,1.8,4.8,3.2c0.8,0.8,1.5,1.7,2.1,2.6 c0.1,0.2,0.4,0.4,0.7,0.4h2.7c0.3,0,0.5-0.3,0.4-0.6C34.9,5.9,28.6,1.9,21.7,1.8C11.4,1.7,3,10.1,2.9,20.3 c0,10.2,8.3,18.5,18.5,18.5c7.1,0,13.4-4,16.5-10.2C38.1,28.3,37.9,28,37.6,28L37.6,28z""/></svg>    

                            Aplikuj szybko
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
            <span class=""offer-badge badge-recommended"">Polecana</span>
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>25 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1680319)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1665747""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2963860/order-picker-%E2%80%93-warzywa-i-owoce-umowa-o-prace-duijndam-works-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZyQNHLUHtO6BzThig1IglhQCN9WR2F0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168776905289554400.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Duijndam Works Sp. z o.o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2963860/order-picker-%E2%80%93-warzywa-i-owoce-umowa-o-prace-duijndam-works-sp-z-o-o-""
                class=""offer-title""
                title=""Order picker – warzywa i owoce""
                
            >
                            Order picker – warzywa i owoce
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Duijndam Works Sp. z o.o.
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    









    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Holandia</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-signature"" class=""svg-inline--fa fa-file-signature fa-md text-brand"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M64 480H296.2C305.1 491.8 317.3 502.3 329.7 511.3C326.6 511.7 323.3 512 320 512H64C28.65 512 0 483.3 0 448V64C0 28.65 28.65 0 64 0H220.1C232.8 0 245.1 5.057 254.1 14.06L369.9 129.9C378.9 138.9 384 151.2 384 163.9V198.6C372.8 201.8 362.1 206 352 211.2V192H240C213.5 192 192 170.5 192 144V32H64C46.33 32 32 46.33 32 64V448C32 465.7 46.33 480 64 480V480zM347.3 152.6L231.4 36.69C229.4 34.62 226.8 33.18 224 32.48V144C224 152.8 231.2 160 240 160H351.5C350.8 157.2 349.4 154.6 347.3 152.6zM454.6 368L491.3 404.7C497.6 410.9 497.6 421.1 491.3 427.3C485.1 433.6 474.9 433.6 468.7 427.3L432 390.6L395.3 427.3C389.1 433.6 378.9 433.6 372.7 427.3C366.4 421.1 366.4 410.9 372.7 404.7L409.4 368L372.7 331.3C366.4 325.1 366.4 314.9 372.7 308.7C378.9 302.4 389.1 302.4 395.3 308.7L432 345.4L468.7 308.7C474.9 302.4 485.1 302.4 491.3 308.7C497.6 314.9 497.6 325.1 491.3 331.3L454.6 368zM576 368C576 447.5 511.5 512 432 512C352.5 512 288 447.5 288 368C288 288.5 352.5 224 432 224C511.5 224 576 288.5 576 368zM432 256C370.1 256 320 306.1 320 368C320 429.9 370.1 480 432 480C493.9 480 544 429.9 544 368C544 306.1 493.9 256 432 256z""/></svg>
    

                            Aplikuj bez CV
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>25 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1665747)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                



<li
    x-ref=""offer-1679158""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2977270/magazynier-komisjoner-art-spozywczych-umowa-o-prace-balticpersonal-gmbh""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZyQNHOUHtO5R_ThiU0IgJhQCV4VBiF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/165476415989309100.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Balticpersonal GmbH""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2977270/magazynier-komisjoner-art-spozywczych-umowa-o-prace-balticpersonal-gmbh""
                class=""offer-title""
                title=""Magazynier/ Komisjoner art. Spożywczych""
                
            >
                            Magazynier/ Komisjoner art. Spożywczych
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Balticpersonal GmbH
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    
    


<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Niemcy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li>
                </ul>

                                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>24 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1679158)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                         



<li
    x-ref=""offer-1677999""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2976111/magazynier-z-udt-m-k-x-umowa-o-prace-raben-logistics-polska-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbZyQNDIUH5A5E6HgSZkIVlrSiIqDBmHgimtA3nM5OOEEgiCCgZGpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/484ec17a6b234ba02a87386f84d84c3c.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Raben Logistics Polska Sp. z o.o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2976111/magazynier-z-udt-m-k-x-umowa-o-prace-raben-logistics-polska-sp-z-o-o-""
                class=""offer-title""
                title=""Magazynier z UDT (m/k/x*)""
                
            >
                            Magazynier z UDT (m/k/x*)
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Raben Logistics Polska Sp. z o.o.
                
                                                                                                                        
            <span class=""block sm:inline-block sm:ml-4"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512"" class=""svg-inline--fa  text-secondary text-base"" style=""""><path fill=""currentColor"" d=""M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zm47-320.7l105.1 15.3-76.1 74.2 18 104.7L256 336l-94 49.4 18-104.7-76.1-74.2L209 191.3 256 96l47 95.3z""/></svg>    

        </span>
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    









    


    <li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Sosnowiec</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--video "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""youtube"" class=""svg-inline--fa fa-youtube icon mr-2 sm:mr-1 text-red-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M549.655 124.083c-6.281-23.65-24.787-42.276-48.284-48.597C458.781 64 288 64 288 64S117.22 64 74.629 75.486c-23.497 6.322-42.003 24.947-48.284 48.597-11.412 42.867-11.412 132.305-11.412 132.305s0 89.438 11.412 132.305c6.281 23.65 24.787 41.5 48.284 47.821C117.22 448 288 448 288 448s170.78 0 213.371-11.486c23.497-6.321 42.003-24.171 48.284-47.821 11.412-42.867 11.412-132.305 11.412-132.305s0-89.438-11.412-132.305zm-317.51 213.508V175.185l142.739 81.205-142.739 81.201z""></path></svg>    

            <span class=""text-sm"">wideo</span>
        </li>
                </ul>

                                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>23 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1677999)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                        



<li
    x-ref=""offer-1673399""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2971512/magazynier-%E2%80%93-operator-wozka-widlowego-%E2%80%93-uprawnienia-udt-m-k-x-umowa-o-prace-raben-logistics-polska-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbZyQNDIUH5A5E6HgSZkIVlrSiIqDBmHgimtA3nM5OOEEgiCCgZGpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/484ec17a6b234ba02a87386f84d84c3c.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Raben Logistics Polska Sp. z o.o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2971512/magazynier-%E2%80%93-operator-wozka-widlowego-%E2%80%93-uprawnienia-udt-m-k-x-umowa-o-prace-raben-logistics-polska-sp-z-o-o-""
                class=""offer-title""
                title=""Magazynier – Operator Wózka Widłowego – uprawnienia UDT (m/k/x*)""
                
            >
                            Magazynier – Operator Wózka Widłowego – uprawnienia UDT (m/k/x*)
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Raben Logistics Polska Sp. z o.o.
                
                                                                                                                        
            <span class=""block sm:inline-block sm:ml-4"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512"" class=""svg-inline--fa  text-secondary text-base"" style=""""><path fill=""currentColor"" d=""M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zm47-320.7l105.1 15.3-76.1 74.2 18 104.7L256 336l-94 49.4 18-104.7-76.1-74.2L209 191.3 256 96l47 95.3z""/></svg>    

        </span>
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    









    


    <li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Gliwice</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--video "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""youtube"" class=""svg-inline--fa fa-youtube icon mr-2 sm:mr-1 text-red-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M549.655 124.083c-6.281-23.65-24.787-42.276-48.284-48.597C458.781 64 288 64 288 64S117.22 64 74.629 75.486c-23.497 6.322-42.003 24.947-48.284 48.597-11.412 42.867-11.412 132.305-11.412 132.305s0 89.438 11.412 132.305c6.281 23.65 24.787 41.5 48.284 47.821C117.22 448 288 448 288 448s170.78 0 213.371-11.486c23.497-6.321 42.003-24.171 48.284-47.821 11.412-42.867 11.412-132.305 11.412-132.305s0-89.438-11.412-132.305zm-317.51 213.508V175.185l142.739 81.205-142.739 81.201z""></path></svg>    

            <span class=""text-sm"">wideo</span>
        </li>
                </ul>

                                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>23 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1673399)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                            </ul>
        </div>
    </section>


                            
<script>
    function showRecommendedPopup() {
        setTimeout(() => {
            window.dispatchEvent(
                new CustomEvent(AplikujApp.events.RECOMMENDED_EXIT_POPUP_DISPLAY)
            );
        }, 1500);
    }
</script>
<div class=""flex justify-center"">
    <div
        x-cloak
        x-data=""recommendedOffersExitPopup""
        x-show=""isVisible""
        x-bind=""eventListeners""
        @keydown.escape.prevent.stop=""hidePopup()""
        role=""dialog""
        aria-modal=""true""
        class=""fixed inset-0 overflow-y-auto z-100""
    >
        <div
            x-show=""isVisible""
            x-transition.opacity
            class=""modal-overlay""
            aria-hidden=""true""
        ></div>

        <div
            x-show=""isVisible""
            x-transition
            @click=""hidePopup()""
            class=""relative min-h-screen flex items-center justify-center p-4 sm:p-8""
        >
            <div
                x-on:click.stop
                x-trap.noscroll.inert=""isVisible""
                class=""w-full h-full sm:max-w-5xl sm:h-auto bg-bgGray rounded-lg shadow-lg p-2 sm:p-8 overflow-y-auto""
            >
                <div class=""flex items-center justify-between"">
                    <div class=""flex-1 min-w-0"">&nbsp;</div>
                    <div class=""flex mt-0 ml-4"">
                        <button
                            type=""button""
                            class=""rounded-md text-gray-500 hover:text-gray-400 px-2 focus:outline-none focus:ring-0""
                            @click=""hidePopup()""
                        >
                            <span class=""sr-only"">Zamknij</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times"" class=""svg-inline--fa fa-times fa-lg"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512""><path fill=""currentColor"" d=""M193.94 256L296.5 153.44l21.15-21.15c3.12-3.12 3.12-8.19 0-11.31l-22.63-22.63c-3.12-3.12-8.19-3.12-11.31 0L160 222.06 36.29 98.34c-3.12-3.12-8.19-3.12-11.31 0L2.34 120.97c-3.12 3.12-3.12 8.19 0 11.31L126.06 256 2.34 379.71c-3.12 3.12-3.12 8.19 0 11.31l22.63 22.63c3.12 3.12 8.19 3.12 11.31 0L160 289.94 262.56 392.5l21.15 21.15c3.12 3.12 8.19 3.12 11.31 0l22.63-22.63c3.12-3.12 3.12-8.19 0-11.31L193.94 256z""></path></svg>    

                        </button>
                    </div>
                </div>

                <div>
                    <h2 class=""font-semibold text-center text-primary"">
                        Przenieśliśmy Cię na zewnętrzny formularz, z którego korzysta pracodawca do zbierania CV kandydatów.
                    </h2>

                    <div class=""text-sm py-4 font-medium"">Oferty pracy, które mogą Cię zainteresować:</div>

                                        <ul role=""list"" class=""offer-list"">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        



<li
    x-ref=""offer-1655190""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2953304/pracownik-magazynu-sortowanie-paczek-dhl-bez-angielskiego-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZwQNHJUHtP4hndgyY0IwxpTiV6XBCF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/172293714707321900.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""Carriere Contracting International Recruitment Polska Sp. z o. o.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2953304/pracownik-magazynu-sortowanie-paczek-dhl-bez-angielskiego-umowa-o-prace-tymczasowa-carriere-contracting-international-recruitment-polska-sp-z-o-o-""
                class=""offer-title""
                title=""Pracownik magazynu - Sortowanie paczek DHL - Bez angielskiego""
                
            >
                            Pracownik magazynu - Sortowanie paczek DHL - Bez angielskiego
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    Carriere Contracting International Recruitment Pol...
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    
    
    

<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Holandia</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę tymczasową</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workImmediately "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M48 416c-8.82 0-16-7.18-16-16V256h160v40c0 13.25 10.75 24 24 24h80c13.25 0 24-10.75 24-24v-40h40.23c10.06-12.19 21.81-22.9 34.77-32H32v-80c0-8.82 7.18-16 16-16h416c8.82 0 16 7.18 16 16v48.81c5.28-.48 10.6-.81 16-.81s10.72.33 16 .81V144c0-26.51-21.49-48-48-48H352V24c0-13.26-10.74-24-24-24H184c-13.26 0-24 10.74-24 24v72H48c-26.51 0-48 21.49-48 48v256c0 26.51 21.49 48 48 48h291.37a174.574 174.574 0 0 1-12.57-32H48zm176-160h64v32h-64v-32zM192 32h128v64H192V32zm358.29 320H512v-54.29c0-5.34-4.37-9.71-9.71-9.71h-12.57c-5.34 0-9.71 4.37-9.71 9.71v76.57c0 5.34 4.37 9.71 9.71 9.71h60.57c5.34 0 9.71-4.37 9.71-9.71v-12.57c0-5.34-4.37-9.71-9.71-9.71zM496 224c-79.59 0-144 64.41-144 144s64.41 144 144 144 144-64.41 144-144-64.41-144-144-144zm0 256c-61.76 0-112-50.24-112-112s50.24-112 112-112 112 50.24 112 112-50.24 112-112 112z""/></svg>    

            <span class=""text-sm"">praca od zaraz</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--inexperience "">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 640 512"" class=""svg-inline--fa  icon mr-2 sm:mr-1 text-gray-500"" style=""""><path fill=""currentColor"" d=""M638.9 209.7l-8-13.9c-2.2-3.8-7.1-5.1-10.9-2.9l-108 63V240c0-26.5-21.5-48-48-48H320v62.2c0 16-10.9 30.8-26.6 33.3-20 3.3-37.4-12.2-37.4-31.6v-94.3c0-13.8 7.1-26.6 18.8-33.9l33.4-20.9c11.4-7.1 24.6-10.9 38.1-10.9h103.2l118.5-67c3.8-2.2 5.2-7.1 3-10.9l-8-14c-2.2-3.8-7.1-5.2-10.9-3l-111 63h-94.7c-19.5 0-38.6 5.5-55.1 15.8l-33.5 20.9c-17.5 11-28.7 28.6-32.2 48.5l-62.5 37c-21.6 13-35.1 36.7-35.1 61.9v38.6L4 357.1c-3.8 2.2-5.2 7.1-3 10.9l8 13.9c2.2 3.8 7.1 5.2 10.9 3L160 305.3v-57.2c0-14 7.5-27.2 19.4-34.4l44.6-26.4v65.9c0 33.4 24.3 63.3 57.6 66.5 38.1 3.7 70.4-26.3 70.4-63.7v-32h112c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16h-24v36c0 19.8-16 35.8-35.8 35.8h-16.1v16c0 22.2-18 40.2-40.2 40.2H238.5l-115.6 67.3c-3.8 2.2-5.1 7.1-2.9 10.9l8 13.8c2.2 3.8 7.1 5.1 10.9 2.9L247.1 448h100.8c34.8 0 64-24.8 70.8-57.7 30.4-6.7 53.3-33.9 53.3-66.3v-4.7c13.6-2.3 24.6-10.6 31.8-21.7l132.2-77c3.8-2.2 5.1-7.1 2.9-10.9z""/></svg>    

            <span class=""text-sm"">bez doświadczenia</span>
        </li>
                </ul>

                                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span class=""offer-card-date font-medium"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512"" class=""svg-inline--fa  fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M240 320C240 328.8 232.8 336 223.1 336C215.2 336 207.1 328.8 207.1 320V208C207.1 199.2 215.2 192 223.1 192C232.8 192 240 199.2 240 208V320zM127.1 16C127.1 7.164 135.2 0 143.1 0H304C312.8 0 320 7.164 320 16C320 24.84 312.8 32 304 32H240V96.61C289.4 100.4 333.1 121.4 367.7 153.6L404.7 116.7C410.9 110.4 421.1 110.4 427.3 116.7C433.6 122.9 433.6 133.1 427.3 139.3L389.1 177.5C416 212.6 432 256.4 432 304C432 418.9 338.9 512 224 512C109.1 512 16 418.9 16 304C16 194.5 100.6 104.8 208 96.61V32H144C135.2 32 128 24.84 128 16H127.1zM223.1 480C321.2 480 400 401.2 400 304C400 206.8 321.2 128 223.1 128C126.8 128 47.1 206.8 47.1 304C47.1 401.2 126.8 480 223.1 480z""/></svg>
    

                            Wygasa <time>za 1 dzień</time>
                        </span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1655190)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        



<li
    x-ref=""offer-1675654""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2973766/komisjoner-magazyn-z-warzywami-i-owocami-emstek-60km-od-bremy-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHOUHtO6BLVhic1Ig1vTCR8XBGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168916605665241800.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""STERNJOB SP. Z O.O.""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2973766/komisjoner-magazyn-z-warzywami-i-owocami-emstek-60km-od-bremy-umowa-o-prace-sternjob-sp-z-o-o-""
                class=""offer-title""
                title=""Komisjoner - Magazyn z warzywami i owocami - Emstek 60km od Bremy""
                
            >
                            Komisjoner - Magazyn z warzywami i owocami - Emstek 60km od Bremy
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    STERNJOB SP. Z O.O.
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

                    
    


    
    
    








    



<li class=""offer-card-labels-list-item offer-card-labels-list-item--salary "">
                    
                

            <span class=""text-sm""><span class=""offer-salary font-bold "">            1 800 do 2 200 € <span class=""font-normal"">netto / mies.</span>
        </span></span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Niemcy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 46.7 42"" class=""svg-inline--fa fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M44.2,15.6H32.1c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h12.1c0.2,0,0.4-0.2,0.4-0.4V16 C44.6,15.8,44.4,15.6,44.2,15.6z M37.9,22h-5.8c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h5.8c0.2,0,0.4-0.2,0.4-0.4v-2.3 C38.3,22.2,38.1,22,37.9,22z M22.1,11.4h-2c-0.3,0-0.5,0.2-0.5,0.5v11.7c0,0.2,0.1,0.3,0.2,0.4l7,5.1c0.2,0.2,0.6,0.1,0.7-0.1 l1.2-1.7v0c0.2-0.2,0.1-0.6-0.1-0.7l-6-4.3V11.9C22.6,11.6,22.4,11.4,22.1,11.4L22.1,11.4z""/><path fill=""currentColor"" d=""M37.6,28h-2.7c-0.3,0-0.5,0.1-0.7,0.4c-0.6,1-1.3,1.8-2.1,2.6c-1.4,1.4-3,2.5-4.8,3.2c-1.9,0.8-3.8,1.2-5.9,1.2 c-2,0-4-0.4-5.9-1.2c-1.8-0.8-3.4-1.8-4.8-3.2s-2.5-3-3.2-4.8c-0.8-1.9-1.2-3.8-1.2-5.9c0-2,0.4-4,1.2-5.9c0.8-1.8,1.8-3.4,3.2-4.8 s3-2.5,4.8-3.2c1.9-0.8,3.8-1.2,5.9-1.2c2,0,4,0.4,5.9,1.2c1.8,0.8,3.4,1.8,4.8,3.2c0.8,0.8,1.5,1.7,2.1,2.6 c0.1,0.2,0.4,0.4,0.7,0.4h2.7c0.3,0,0.5-0.3,0.4-0.6C34.9,5.9,28.6,1.9,21.7,1.8C11.4,1.7,3,10.1,2.9,20.3 c0,10.2,8.3,18.5,18.5,18.5c7.1,0,13.4-4,16.5-10.2C38.1,28.3,37.9,28,37.6,28L37.6,28z""/></svg>    

                            Aplikuj szybko
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
            <span class=""offer-badge badge-recommended"">Polecana</span>
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>26 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1675654)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        



<li
    x-ref=""offer-1671161""
    data-test
    class=""offer-card""
>
    <div class=""offer-card-content"">
        


    


<div class=""offer-card-main-wrapper"">

    <div class=""offer-card-thumb"">
        <div class=""offer-card-thumb-box"">
            <a
                href=""/oferta/2969274/magazynier-komisjoner-okolice-hamburga-18-%E2%82%AC-brutto-bez-kwatery-umowa-o-prace-actuell-personal-gmbh""
                class=""py-2 leading-4""
                
            >    
                                <img
                loading=""lazy""
                src=""/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbZyQNHOUHhNsxjUgyZkdV5sTiF5CBrW2nT8AyTK5L7TEwLUC1cSpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/25c3037abe5771e3c9ef3e0fece2bbb7.webp""
                class=""offer-card-company-logo text-2xs""
                alt=""actuell Personal GmbH""
            >
            </a>
        </div>
    </div>

    <div class=""offer-card-main"">
        <h3>
            <a
                href=""/oferta/2969274/magazynier-komisjoner-okolice-hamburga-18-%E2%82%AC-brutto-bez-kwatery-umowa-o-prace-actuell-personal-gmbh""
                class=""offer-title""
                title=""Magazynier/Komisjoner (okolice Hamburga) 18 € brutto bez kwatery""
                
            >
                            Magazynier/Komisjoner (okolice Hamburga) 18 € brutto bez kwatery
    
            </a>
        </h3>

        <div class=""flex flex-row"">
            
            <div class=""text-sm"">
                                    actuell Personal GmbH
                
                                                                                                                        
    
                                                            
                <ul class=""offer-card-labels-list"">
                    

        



    
    
    








    



<li class=""offer-card-labels-list-item offer-card-labels-list-item--workPlace "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""map-marker-alt"" class=""svg-inline--fa fa-map-marker-alt icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M192 96c-52.935 0-96 43.065-96 96s43.065 96 96 96 96-43.065 96-96-43.065-96-96-96zm0 160c-35.29 0-64-28.71-64-64s28.71-64 64-64 64 28.71 64 64-28.71 64-64 64zm0-256C85.961 0 0 85.961 0 192c0 77.413 26.97 99.031 172.268 309.67 9.534 13.772 29.929 13.774 39.465 0C357.03 291.031 384 269.413 384 192 384 85.961 298.039 0 192 0zm0 473.931C52.705 272.488 32 256.494 32 192c0-42.738 16.643-82.917 46.863-113.137S149.262 32 192 32s82.917 16.643 113.137 46.863S352 149.262 352 192c0 64.49-20.692 80.47-160 281.931z""></path></svg>    

            <span class=""text-sm"">Niemcy</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--employmentType "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""file-contract"" class=""svg-inline--fa fa-file-contract icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 384 512""><path fill=""currentColor"" d=""M196.66 363.33l-13.88-41.62c-3.28-9.81-12.44-16.41-22.78-16.41s-19.5 6.59-22.78 16.41L119 376.36c-1.5 4.58-5.78 7.64-10.59 7.64H96c-8.84 0-16 7.16-16 16s7.16 16 16 16h12.41c18.62 0 35.09-11.88 40.97-29.53L160 354.58l16.81 50.48a15.994 15.994 0 0 0 14.06 10.89c.38.03.75.05 1.12.05 6.03 0 11.59-3.41 14.31-8.86l7.66-15.33c2.78-5.59 7.94-6.19 10.03-6.19s7.25.59 10.19 6.53c7.38 14.7 22.19 23.84 38.62 23.84H288c8.84 0 16-7.16 16-16s-7.16-16-16-16h-15.19c-4.28 0-8.12-2.38-10.16-6.5-11.96-23.85-46.24-30.33-65.99-14.16zM72 96h112c4.42 0 8-3.58 8-8V72c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8zm120 56v-16c0-4.42-3.58-8-8-8H72c-4.42 0-8 3.58-8 8v16c0 4.42 3.58 8 8 8h112c4.42 0 8-3.58 8-8zm177.9-54.02L286.02 14.1c-9-9-21.2-14.1-33.89-14.1H47.99C21.5.1 0 21.6 0 48.09v415.92C0 490.5 21.5 512 47.99 512h288.02c26.49 0 47.99-21.5 47.99-47.99V131.97c0-12.69-5.1-24.99-14.1-33.99zM256.03 32.59c2.8.7 5.3 2.1 7.4 4.2l83.88 83.88c2.1 2.1 3.5 4.6 4.2 7.4h-95.48V32.59zm95.98 431.42c0 8.8-7.2 16-16 16H47.99c-8.8 0-16-7.2-16-16V48.09c0-8.8 7.2-16.09 16-16.09h176.04v104.07c0 13.3 10.7 23.93 24 23.93h103.98v304.01z""></path></svg>    

            <span class=""text-sm"">umowa o pracę</span>
        </li><li class=""offer-card-labels-list-item offer-card-labels-list-item--onlineRecruitment "">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""video"" class=""svg-inline--fa fa-video icon mr-2 sm:mr-1 text-gray-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M543.9 96c-6.2 0-12.5 1.8-18.2 5.7L416 171.6v-59.8c0-26.4-23.2-47.8-51.8-47.8H51.8C23.2 64 0 85.4 0 111.8v288.4C0 426.6 23.2 448 51.8 448h312.4c28.6 0 51.8-21.4 51.8-47.8v-59.8l109.6 69.9c5.7 4 12.1 5.7 18.2 5.7 16.6 0 32.1-13 32.1-31.5v-257c.1-18.5-15.4-31.5-32-31.5zM384 400.2c0 8.6-9.1 15.8-19.8 15.8H51.8c-10.7 0-19.8-7.2-19.8-15.8V111.8c0-8.6 9.1-15.8 19.8-15.8h312.4c10.7 0 19.8 7.2 19.8 15.8v288.4zm160-15.7l-1.2-1.3L416 302.4v-92.9L544 128v256.5z""></path></svg>    

            <span class=""text-sm"">rekrutacja zdalna</span>
        </li>
                </ul>

                                                            <div class=""pt-2 lg:pt-1"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 46.7 42"" class=""svg-inline--fa fa-md text-brand"" style=""""><path fill=""currentColor"" d=""M44.2,15.6H32.1c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h12.1c0.2,0,0.4-0.2,0.4-0.4V16 C44.6,15.8,44.4,15.6,44.2,15.6z M37.9,22h-5.8c-0.2,0-0.4,0.2-0.4,0.4v2.3c0,0.2,0.2,0.4,0.4,0.4h5.8c0.2,0,0.4-0.2,0.4-0.4v-2.3 C38.3,22.2,38.1,22,37.9,22z M22.1,11.4h-2c-0.3,0-0.5,0.2-0.5,0.5v11.7c0,0.2,0.1,0.3,0.2,0.4l7,5.1c0.2,0.2,0.6,0.1,0.7-0.1 l1.2-1.7v0c0.2-0.2,0.1-0.6-0.1-0.7l-6-4.3V11.9C22.6,11.6,22.4,11.4,22.1,11.4L22.1,11.4z""/><path fill=""currentColor"" d=""M37.6,28h-2.7c-0.3,0-0.5,0.1-0.7,0.4c-0.6,1-1.3,1.8-2.1,2.6c-1.4,1.4-3,2.5-4.8,3.2c-1.9,0.8-3.8,1.2-5.9,1.2 c-2,0-4-0.4-5.9-1.2c-1.8-0.8-3.4-1.8-4.8-3.2s-2.5-3-3.2-4.8c-0.8-1.9-1.2-3.8-1.2-5.9c0-2,0.4-4,1.2-5.9c0.8-1.8,1.8-3.4,3.2-4.8 s3-2.5,4.8-3.2c1.9-0.8,3.8-1.2,5.9-1.2c2,0,4,0.4,5.9,1.2c1.8,0.8,3.4,1.8,4.8,3.2c0.8,0.8,1.5,1.7,2.1,2.6 c0.1,0.2,0.4,0.4,0.7,0.4h2.7c0.3,0,0.5-0.3,0.4-0.6C34.9,5.9,28.6,1.9,21.7,1.8C11.4,1.7,3,10.1,2.9,20.3 c0,10.2,8.3,18.5,18.5,18.5c7.1,0,13.4-4,16.5-10.2C38.1,28.3,37.9,28,37.6,28L37.6,28z""/></svg>    

                            Aplikuj szybko
                        </div>
                                    
                            </div>
        </div>
    </div>

    <div class=""offer-card-right-col"">
        <div class=""offer-card-right-col-wrapper"">
            <div>
                <div class=""mb-2"">    
    
    </div>
                <div class=""flex flex-col text-xs"">
                                            <span><span class=""offer-card-date"">Dodana</span> <time>26 września</time></span>
                                    </div>
            </div>
            <div class=""offer-card-actions"">
                                    <div class=""z-1 relative"">
                            
    <button
        x-cloak
        x-data=""observedButton""
        x-init=""initialize(1671161)""
        x-bind=""eventListeners""
        :title=""buttonTitle""
        @click.prevent=""toggleObservedState()""
        class=""inline-flex items-center ml-2""
        type=""button""
    >
        <span :class=""{ 'hidden': isObserved }"">
                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  fa-lg"" style="""" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M287.9 435.9L150.1 509.1C142.9 513.4 133.1 512.7 125.6 507.4C118.2 502.1 114.5 492.9 115.1 483.9L142.2 328.4L31.11 218.2C24.65 211.9 22.36 202.4 25.2 193.7C28.03 185.1 35.5 178.8 44.49 177.5L197.7 154.8L266.3 13.52C270.4 5.249 278.7 0 287.9 0C297.1 0 305.5 5.25 309.5 13.52L378.1 154.8L531.4 177.5C540.4 178.8 547.8 185.1 550.7 193.7C553.5 202.4 551.2 211.9 544.8 218.2L433.6 328.4L459.9 483.9C461.4 492.9 457.7 502.1 450.2 507.4C442.8 512.7 432.1 513.4 424.9 509.1L287.9 435.9zM226.5 168.8C221.9 178.3 212.9 184.9 202.4 186.5L64.99 206.8L164.8 305.6C172.1 312.9 175.5 323.4 173.8 333.7L150.2 473.2L272.8 407.7C282.3 402.6 293.6 402.6 303 407.7L425.6 473.2L402.1 333.7C400.3 323.4 403.7 312.9 411.1 305.6L510.9 206.8L373.4 186.5C362.1 184.9 353.1 178.3 349.3 168.8L287.9 42.32L226.5 168.8z""/></svg>
    

            <span class=""hidden sm:inline-block"">obserwuj</span>
        </span>

        <span :class=""{ 'hidden': !isObserved }"">
                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fas"" data-icon=""star"" class=""svg-inline--fa fa-star text-primary fa-lg"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 576 512""><path fill=""currentColor"" d=""M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z""></path></svg>
    

            <span class=""hidden sm:inline-block"">obserwujesz</span>
        </span>
    </button>

                    </div>
                            </div>
        </div>
    </div>
</div>



    </div>
</li>
                                            </ul>
                </div>
            </div>
        </div>
    </div>
</div>

            
    
        <section class=""section section--condensed bg-bgGray"">
        <div class=""container max-w-7xl mx-auto"">
            
    <div class=""py-4"">
                <div class=""relative"">
        <div class=""absolute inset-0 flex items-center"" aria-hidden=""true"">
            <div class=""w-full border-t border-gray-300""></div>
        </div>
        <div class=""relative flex justify-start"">
            <span class=""pr-3 bg-bgGray text-lg font-medium text-gray-900"">
                  <h2>    Praca w sąsiednich miejscowościach
</h2>

            </span>
        </div>
    </div>

    </div>
    <ul role=""list"" class=""grid-links"">
        <li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/myslowice""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Mysłowice""
        >
            Praca Mysłowice
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/bedzin""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Będzin""
        >
            Praca Będzin
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/czeladz""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Czeladź""
        >
            Praca Czeladź
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/dabrowa-gornicza""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Dąbrowa Górnicza""
        >
            Praca Dąbrowa Górnicza
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/siemianowice-slaskie""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Siemianowice Śląskie""
        >
            Praca Siemianowice Śląskie
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/katowice""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Katowice""
        >
            Praca Katowice
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/sarnow-gmina-psary-powiat-bedzinski""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Sarnów""
        >
            Praca Sarnów
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/preczow""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Preczów""
        >
            Praca Preczów
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/wojkowice""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Wojkowice""
        >
            Praca Wojkowice
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/psary""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Psary""
        >
            Praca Psary
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/strzyzowice-gmina-psary-powiat-bedzinski""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Strzyżowice""
        >
            Praca Strzyżowice
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/chorzow""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Chorzów""
        >
            Praca Chorzów
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/jaworzno""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Jaworzno""
        >
            Praca Jaworzno
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/rogoznik-gmina-bobrowniki-powiat-bedzinski""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Rogoźnik""
        >
            Praca Rogoźnik
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/bobrowniki""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Bobrowniki""
        >
            Praca Bobrowniki
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/imielin""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Imielin""
        >
            Praca Imielin
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/swietochlowice""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Świętochłowice""
        >
            Praca Świętochłowice
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/dobieszowice-gmina-bobrowniki-powiat-bedzinski""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Dobieszowice""
        >
            Praca Dobieszowice
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/ledziny""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Lędziny""
        >
            Praca Lędziny
                    </a>
    </h3>
</li>
<li class=""col-span-1 flex"">
    <h3 class=""w-full"">
        <a
            href=""/praca/siemonia""
            class=""link-list fade font-light""
            title=""Aktualne oferty pracy Siemonia""
        >
            Praca Siemonia
                    </a>
    </h3>
</li>
    </ul>
    <div class=""py-2 sm:py-6""></div>

        </div>
    </section>


            <script type=""application/ld+json"">
    {
        ""@context"": ""https://schema.org"",
        ""@type"": ""BreadcrumbList"",
        ""itemListElement"":
        [
                            {""@type"":""ListItem"",""position"":1,""item"":{""@id"":""https:\/\/www.aplikuj.pl\/praca"",""name"":""Praca""}} ,
                            {""@type"":""ListItem"",""position"":2,""item"":{""@id"":""https:\/\/www.aplikuj.pl\/praca\/slaskie"",""name"":""\u015al\u0105skie""}} ,
                            {""@type"":""ListItem"",""position"":3,""item"":{""@id"":""https:\/\/www.aplikuj.pl\/praca\/sosnowiec"",""name"":""Sosnowiec""}} ,
                            {""@type"":""ListItem"",""position"":4,""item"":{""@id"":""https:\/\/www.aplikuj.pl\/praca\/sosnowiec\/magazynier"",""name"":""Magazynier""}} 
                    ]
    }
</script>

    <script type=""application/ld+json"">
    {
        ""@context"": ""https://schema.org"",
        ""@type"": ""JobPosting"",
        ""title"": ""Magazynier // 5.570 zł // od zaraz"",
        ""hiringOrganization"": ""Gi Group S.A."",
        ""datePosted"": ""2025-09-05"",
        ""validThrough"": ""2025-10-05"",
        ""employmentType"": ""FULL_TIME"",
        ""industry"": ""Magazynier"",
        ""jobLocation"": {
            ""@type"": ""Place"",
            ""address"": {
                ""@type"": ""PostalAddress"",
                                    ""addressLocality"": ""Sosnowiec"",
                    ""addressRegion"": ""Śląskie""
                            }
        },
                        ""baseSalary"":
        {
          ""@type"": ""MonetaryAmount"",
          ""currency"": ""PLN"",
          ""value"": {
            ""@type"": ""QuantitativeValue"",
            ""minValue"": 4,
            ""maxValue"": 5570,
            ""unitText"": ""MONTH""
          }
        },
                ""directApply"": false,
        ""occupationalCategory"": ""Magazyn"",
        ""salaryCurrency"": ""PLN"",
        ""description"": ""
        
    
                        
    &lt;div class=&quot;pt-6&quot;&gt;
                &lt;h2 class=&quot;pb-2 font-bold&quot;&gt;
        Zakres obowiązków
    &lt;/h2&gt;
    &lt;div class=&quot;border-t-4 border-gray-300 w-20 mb-4&quot; style=&quot;border-color: #d1d5db&quot;&gt;&lt;/div&gt;

        &lt;div class=&quot;pb-4 sm:pb-12&quot;&gt;
                        
    
    
    

    
    &lt;p&gt;Zadania:&lt;/p&gt;
&lt;ul&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Kompletacja zam&amp;oacute;wień&lt;/div&gt;&lt;/li&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Przyjmowanie towaru&lt;/div&gt;&lt;/li&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Wydawanie towaru&lt;/div&gt;&lt;/li&gt;
&lt;/ul&gt;
&lt;p&gt;&lt;br /&gt;&lt;br /&gt; &lt;br /&gt;&lt;br /&gt;&lt;/p&gt;



        &lt;/div&gt;
    
                &lt;h2 class=&quot;pb-2 font-bold&quot;&gt;
        Wymagania
    &lt;/h2&gt;
    &lt;div class=&quot;border-t-4 border-gray-300 w-20 mb-4&quot; style=&quot;border-color: #d1d5db&quot;&gt;&lt;/div&gt;

        &lt;div class=&quot;pb-4 sm:pb-12&quot;&gt;
                        
    
    
    

    
    &lt;p&gt;Oczekujemy:&lt;/p&gt;
&lt;ul&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Dyspozycyjności do pracy w systemie 3- zmianowym&lt;/div&gt;&lt;/li&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Zaangażowania w wykonywane obowiązki&lt;/div&gt;&lt;/li&gt;
&lt;/ul&gt;



        &lt;/div&gt;
    
                &lt;h2 class=&quot;pb-2 font-bold&quot;&gt;
        Oferujemy
    &lt;/h2&gt;
    &lt;div class=&quot;border-t-4 border-gray-300 w-20 mb-4&quot; style=&quot;border-color: #d1d5db&quot;&gt;&lt;/div&gt;

        &lt;div class=&quot;pb-4 sm:pb-12&quot;&gt;
                        
    
    
    

    
    &lt;br/&gt;
&lt;ul&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Stabilne zatrudnienie na podstawie umowy o pracę&lt;/div&gt;&lt;/li&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Wynagrodzenie: 5570 zł brutto (podstawa + premia)&lt;/div&gt;&lt;/li&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Premię do 15% wynagrodzenia zasadniczego&lt;/div&gt;&lt;/li&gt;
&lt;li class=&quot;leading-6 flex py-1&quot;&gt;
            &lt;div class=&quot;mr-4 flex-shrink-0&quot;&gt;        
            &lt;svg aria-hidden=&quot;true&quot; focusable=&quot;false&quot; data-prefix=&quot;fal&quot; data-icon=&quot;check-circle&quot; class=&quot;svg-inline--fa fa-check-circle fa-lg text-gray-400 mt-1&quot; style=&quot;&quot; role=&quot;img&quot; xmlns=&quot;&quot; viewBox=&quot;0 0 512 512&quot;&gt;&lt;path fill=&quot;currentColor&quot; d=&quot;M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z&quot;&gt;&lt;/path&gt;&lt;/svg&gt;    
&lt;/div&gt;&lt;div class=&quot;flex-auto&quot;&gt;Pracę w systemie 3- zmianowym od poniedziałku do piątku&lt;/div&gt;&lt;/li&gt;
&lt;/ul&gt;



        &lt;/div&gt;
        &lt;/div&gt;
""
    }
</script>


                </main>
            <footer class=""footer print:hidden"" aria-labelledby=""footer-heading"">
        <span id=""footer-heading"" class=""sr-only"">Stopka</span>
        <div class=""max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:py-16 lg:px-8"">
            <div class=""md:flex md:flex-row xl:gap-12"">
                <div class=""space-y-8 xl:col-span-1"">
                    <div class=""bg-white rounded-full py-2 px-3 w-full flex justify-center"">
                        <a href=""https://www.aplikuj.pl"" title=""Aplikuj.pl"">
                            <img class=""h-[50px]"" width=""185"" height=""50"" src=""/build/logo/logo-blue.80d8c0e2.svg"" alt=""Aplikuj.pl"" loading=""lazy"">
                        </a>
                    </div>
                    <p class=""text-white text-base"">
                        Dobrze Cię poznać!<br>
                        Znajdź ofertę pracy i APLIKUJ.
                    </p>
                    <div class=""flex space-x-6"">
                        <a href=""https://www.facebook.com/aplikujpl"" class=""sm-icon"" rel=""nofollow"" target=""_blank"">
                            <span class=""sr-only"">Facebook</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""facebook"" class=""svg-inline--fa fa-facebook fa-2x"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M504 256C504 119 393 8 256 8S8 119 8 256c0 123.78 90.69 226.38 209.25 245V327.69h-63V256h63v-54.64c0-62.15 37-96.48 93.67-96.48 27.14 0 55.52 4.84 55.52 4.84v61h-31.28c-30.8 0-40.41 19.12-40.41 38.73V256h68.78l-11 71.69h-57.78V501C413.31 482.38 504 379.78 504 256z""></path></svg>    

                        </a>
                        <a href=""https://www.linkedin.com/company/11437570/"" class=""sm-icon"" rel=""nofollow""
                           target=""_blank"">
                            <span class=""sr-only"">Linked In</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""linkedin-in"" class=""svg-inline--fa fa-linkedin-in fa-2x"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M100.28 448H7.4V148.9h92.88zM53.79 108.1C24.09 108.1 0 83.5 0 53.8a53.79 53.79 0 0 1 107.58 0c0 29.7-24.1 54.3-53.79 54.3zM447.9 448h-92.68V302.4c0-34.7-.7-79.2-48.29-79.2-48.29 0-55.69 37.7-55.69 76.7V448h-92.78V148.9h89.08v40.8h1.3c12.4-23.5 42.69-48.3 87.88-48.3 94 0 111.28 61.9 111.28 142.3V448z""></path></svg>    

                        </a>
                        <a href=""https://www.instagram.com/aplikuj.pl"" class=""sm-icon"" rel=""nofollow"" target=""_blank"">
                            <span class=""sr-only"">Instagram</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fab"" data-icon=""instagram"" class=""svg-inline--fa fa-instagram fa-2x"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M224.1 141c-63.6 0-114.9 51.3-114.9 114.9s51.3 114.9 114.9 114.9S339 319.5 339 255.9 287.7 141 224.1 141zm0 189.6c-41.1 0-74.7-33.5-74.7-74.7s33.5-74.7 74.7-74.7 74.7 33.5 74.7 74.7-33.6 74.7-74.7 74.7zm146.4-194.3c0 14.9-12 26.8-26.8 26.8-14.9 0-26.8-12-26.8-26.8s12-26.8 26.8-26.8 26.8 12 26.8 26.8zm76.1 27.2c-1.7-35.9-9.9-67.7-36.2-93.9-26.2-26.2-58-34.4-93.9-36.2-37-2.1-147.9-2.1-184.9 0-35.8 1.7-67.6 9.9-93.9 36.1s-34.4 58-36.2 93.9c-2.1 37-2.1 147.9 0 184.9 1.7 35.9 9.9 67.7 36.2 93.9s58 34.4 93.9 36.2c37 2.1 147.9 2.1 184.9 0 35.9-1.7 67.7-9.9 93.9-36.2 26.2-26.2 34.4-58 36.2-93.9 2.1-37 2.1-147.8 0-184.8zM398.8 388c-7.8 19.6-22.9 34.7-42.6 42.6-29.5 11.7-99.5 9-132.1 9s-102.7 2.6-132.1-9c-19.6-7.8-34.7-22.9-42.6-42.6-11.7-29.5-9-99.5-9-132.1s-2.6-102.7 9-132.1c7.8-19.6 22.9-34.7 42.6-42.6 29.5-11.7 99.5-9 132.1-9s102.7-2.6 132.1 9c19.6 7.8 34.7 22.9 42.6 42.6 11.7 29.5 9 99.5 9 132.1s2.7 102.7-9 132.1z""></path></svg>    

                        </a>
                        <a href=""https://x.com/aplikuj_pl"" class=""sm-icon"" rel=""nofollow"" target=""_blank"">
                            <span class=""sr-only"">Instagram</span>
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512"" class=""svg-inline--fa fa-twitter fa-2x"" style=""""><path fill=""currentColor"" d=""M389.2 48h70.6L305.6 224.2 487 464H345L233.7 318.6 106.5 464H35.8L200.7 275.5 26.8 48H172.4L272.9 180.9 389.2 48zM364.4 421.8h39.1L151.1 88h-42L364.4 421.8z""/></svg>    

                        </a>
                    </div>
                </div>

                <div class=""mt-12 grid grid-cols-2 gap-8 xl:gap-12 xl:mt-0 md:grid-cols-3"">
                    <div>
                        <h3 class=""header-section"">
                            Dla kandydatów
                        </h3>
                        <ul role=""list"" class=""mt-4 space-y-4"">
                            <li>
                                <a href=""/praca"" class=""nav-link"">
                                    Oferty pracy
                                </a>
                            </li>
                            <li>
                                <a rel=""nofollow"" href=""/kandydat/logowanie"" class=""nav-link"">
                                    Konto kandydata
                                </a>
                            </li>
                            <li>
                                <a href=""/pracodawcy"" class=""nav-link"">
                                    Pracodawcy
                                </a>
                            </li>
                            <li>
                                <a href=""/porady"" class=""nav-link"">
                                    Porady
                                </a>
                            </li>
                            <li>
                                <a href=""/cv/"" class=""nav-link"">
                                    Wzory CV
                                </a>
                            </li>
                            <li>
                                <a href=""/kalkulator-wynagrodzen"" class=""nav-link"">
                                    Kalkulator wynagrodzeń
                                </a>
                            </li>
                            <li>
                                <a href=""/regulamin"" class=""nav-link"" rel=""nofollow"">
                                    Regulamin dla kandydatów
                                </a>
                            </li>
                            <li>
                                <a href=""/powiadomienia"" class=""nav-link"" rel=""nofollow"">
                                    Powiadomienia
                                </a>
                            </li>
                            <li>
                                <a href=""https://play.google.com/store/apps/details?id=pl.aplikuj.mobile""
                                   target=""_blank"" rel=""nofollow"" class=""nav-link"">
                                    Aplikacja mobilna Android
                                </a>
                            </li>
                            <li>
                                <a href=""https://itunes.apple.com/pl/app/aplikuj-pl-oferty-pracy/id1477877286""
                                   target=""_blank"" rel=""nofollow"" class=""nav-link"">
                                    Aplikacja mobilna iOS
                                </a>
                            </li>
                        </ul>
                    </div>

                    <div>
                        <h3 class=""header-section"">
                            Dla pracodawców
                        </h3>
                        <ul role=""list"" class=""mt-4 space-y-4"">
                            <li>
                                <a href=""/dla-pracodawcow"" class=""nav-link"">
                                    Oferta i cennik
                                </a>
                            </li>
                            <li>
                                <a href=""http://profilpracodawcy.aplikuj.pl/"" class=""nav-link"" rel=""nofollow"">
                                    Profil pracodawcy 360&deg;
                                </a>
                            </li>
                            <li>
                                <a href=""/firma"" class=""nav-link"" rel=""nofollow"">
                                    Strefa pracodawcy
                                </a>
                            </li>
                            <li>
                                <a href=""/dokumenty/"" class=""nav-link"">
                                    Wzory dokumentów
                                </a>
                            </li>
                            <li>
                                <a href=""/regulamin-dla-pracodawcow"" class=""nav-link"" rel=""nofollow"">
                                    Regulamin dla pracodawców
                                </a>
                            </li>
                            <li>
                                <a href=""https://pracodawca-app.aplikuj.pl/"" target=""_blank"" rel=""nofollow"" class=""nav-link"">
                                    Aplikacja mobilna
                                </a>
                            </li>
                        </ul>
                    </div>

                    <div>
                        <h3 class=""header-section"">
                            Portal
                        </h3>
                        <ul role=""list"" class=""mt-4 space-y-4"">
                            <li>
                                <a href=""/pracodawca/3532/aplikuj-pl"" class=""nav-link"">
                                    O nas
                                </a>
                            </li>
                            <li>
                                <a href=""/kontakt"" class=""nav-link"">
                                    Kontakt
                                </a>
                            </li>
                            <li>
                                <a href=""/program-partnerski"" class=""nav-link"" rel=""nofollow"">
                                Program partnerski
                                </a>
                            </li>
                            <li>
                                <a href=""/polityka-prywatnosci"" class=""nav-link"" rel=""nofollow"">
                                    Polityka prywatności
                                </a>
                            </li>
                            <li>
                                <a href=""/dotacja"" class=""nav-link"" rel=""nofollow"">
                                    Dotacja
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class=""section-md max-w-3xl mx-auto"">
                <img class=""w-full opacity-90"" src=""/build/images/ue-dotacja.07199cf0.webp"" alt=""Dotacja"">
            </div>

            <div class=""pb-10 sm:pb-0 border-t border-gray-600 pt-8"">
                <p class=""text-xs sm:text-sm text-gray-400 xl:text-center"">
                    &copy;&nbsp;2012-2025&nbsp;Aplikuj.pl&copy;. Wszelkie prawa zastrzeżone.
                </p>
            </div>
        </div>
    </footer>


    
<div
    x-data=""notificationDispatcher""
    x-bind=""eventListeners""
    class=""fixed inset-0 flex items-end px-4 py-16 pointer-events-none sm:items-start notification-container""
>
    <div class=""w-full flex flex-col items-center space-y-4 sm:items-end"">
        <template x-for=""notification in notifications"" :key=""notification.id"">
            <div
                x-data=""notificationItem""
                x-init=""setupNotification(notification)""
                x-show=""show""
                x-transition.duration.200ms
                class=""max-w-xs w-full shadow-md rounded-lg pointer-events-auto overflow-hidden notification""
                :class=""[
                    `notification--${notification.class} notification--${notification.type}`,
                    notification.type === 'success' ? 'bg-green-100' : '',
                    notification.type === 'error' ? 'bg-red-100' : '',
                    notification.type === 'info' ? 'bg-blue-100' : '',
                    notification.type ? '' : 'bg-white border border-gray-200'
                ].join(' ')""
            >
                <div class=""p-4"">
                    <div class=""flex items-start"">
                        <div x-show=""notification.type === 'success'"" class=""flex-shrink-0"">
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""check-circle"" class=""svg-inline--fa fa-check-circle fa-lg text-green-500"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 464c-118.664 0-216-96.055-216-216 0-118.663 96.055-216 216-216 118.664 0 216 96.055 216 216 0 118.663-96.055 216-216 216zm141.63-274.961L217.15 376.071c-4.705 4.667-12.303 4.637-16.97-.068l-85.878-86.572c-4.667-4.705-4.637-12.303.068-16.97l8.52-8.451c4.705-4.667 12.303-4.637 16.97.068l68.976 69.533 163.441-162.13c4.705-4.667 12.303-4.637 16.97.068l8.451 8.52c4.668 4.705 4.637 12.303-.068 16.97z""></path></svg>    

                        </div>

                        <div x-show=""notification.type === 'error'"" class=""flex-shrink-0"">
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times-circle"" class=""svg-inline--fa fa-times-circle fa-lg text-red-500"" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512""><path fill=""currentColor"" d=""M256 8C119 8 8 119 8 256s111 248 248 248 248-111 248-248S393 8 256 8zm0 464c-118.7 0-216-96.1-216-216 0-118.7 96.1-216 216-216 118.7 0 216 96.1 216 216 0 118.7-96.1 216-216 216zm94.8-285.3L281.5 256l69.3 69.3c4.7 4.7 4.7 12.3 0 17l-8.5 8.5c-4.7 4.7-12.3 4.7-17 0L256 281.5l-69.3 69.3c-4.7 4.7-12.3 4.7-17 0l-8.5-8.5c-4.7-4.7-4.7-12.3 0-17l69.3-69.3-69.3-69.3c-4.7-4.7-4.7-12.3 0-17l8.5-8.5c4.7-4.7 12.3-4.7 17 0l69.3 69.3 69.3-69.3c4.7-4.7 12.3-4.7 17 0l8.5 8.5c4.6 4.7 4.6 12.3 0 17z""></path></svg>    

                        </div>

                        <div x-show=""notification.type === 'info'"" class=""flex-shrink-0"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 512 512"" class=""svg-inline--fa  fa-lg text-blue-500"" style=""""><path fill=""currentColor"" d=""M256 40c118.621 0 216 96.075 216 216 0 119.291-96.61 216-216 216-119.244 0-216-96.562-216-216 0-119.203 96.602-216 216-216m0-32C119.043 8 8 119.083 8 256c0 136.997 111.043 248 248 248s248-111.003 248-248C504 119.083 392.957 8 256 8zm-36 344h12V232h-12c-6.627 0-12-5.373-12-12v-8c0-6.627 5.373-12 12-12h48c6.627 0 12 5.373 12 12v140h12c6.627 0 12 5.373 12 12v8c0 6.627-5.373 12-12 12h-72c-6.627 0-12-5.373-12-12v-8c0-6.627 5.373-12 12-12zm36-240c-17.673 0-32 14.327-32 32s14.327 32 32 32 32-14.327 32-32-14.327-32-32-32z""/></svg>    

                        </div>

                        <div class=""ml-3 w-0 flex-1 pt-1"">
                            <p class=""text-sm font-semibold text-gray-900 mb-1"" x-show=""notification.title"" x-text=""notification.title""></p>
                            <p class=""text-sm text-gray-700 font-medium"" x-html=""notification.content""></p>

                            <div class=""mt-4 flex justify-end space-x-7"" x-show=""hasActionButtons()"">
                                <a
                                    x-show=""notification.showObservedAction""
                                    href=""/obserwowane/oferty"" class=""btn btn-outline-primary btn-xs"" title=""""
                                >
                                    Obserwowane
                                </a>

                                <a
                                    x-show=""notification.showNewsletterAction""
                                    href=""/kandydat/ustawienia-powiadomien"" class=""btn btn-outline-primary btn-xs"" title=""""
                                >
                                    Zarządzaj
                                </a>

                                <a
                                    x-show=""notification.showAccountAction""
                                    href=""/kandydat"" class=""btn btn-outline-primary btn-xs"" title=""""
                                >
                                    Zaloguj się                                </a>

                                <button
                                    x-show=""notification.showCloseButton""
                                    class=""btn btn-outline-primary btn-xs"" title=""""
                                    @click=""hideNotification()""
                                >
                                    OK
                                </button>
                            </div>
                        </div>

                        <div class=""ml-4 flex-shrink-0 flex"">
                            <button
                                @click=""hideNotification()""
                                class=""rounded-md inline-flex text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-0""
                            >
                                        
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times"" class=""svg-inline--fa fa-times fa-lg"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512""><path fill=""currentColor"" d=""M193.94 256L296.5 153.44l21.15-21.15c3.12-3.12 3.12-8.19 0-11.31l-22.63-22.63c-3.12-3.12-8.19-3.12-11.31 0L160 222.06 36.29 98.34c-3.12-3.12-8.19-3.12-11.31 0L2.34 120.97c-3.12 3.12-3.12 8.19 0 11.31L126.06 256 2.34 379.71c-3.12 3.12-3.12 8.19 0 11.31l22.63 22.63c3.12 3.12 8.19 3.12 11.31 0L160 289.94 262.56 392.5l21.15 21.15c3.12 3.12 8.19 3.12 11.31 0l22.63-22.63c3.12-3.12 3.12-8.19 0-11.31L193.94 256z""></path></svg>    

                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </template>
    </div>
</div>

    

                        <div x-data=""jobAlertSubscriptionService"" x-bind=""eventListeners""></div>
                                                <div x-data=""jobAlertExitPopupService"" x-bind=""eventListeners""></div>
                
                
<div class=""flex justify-center"">
    <div
        x-cloak
        x-data=""jobAlertSubscriptionPopup""
        x-show=""isVisible""
        x-bind=""eventListeners""
        @keydown.escape.prevent.stop=""hidePopup()""
        role=""dialog""
        aria-modal=""true""
        class=""fixed inset-0 overflow-y-auto z-50""
    >
        <div
            x-show=""isVisible""
            x-transition.opacity
            class=""modal-overlay""
            aria-hidden=""true""
        ></div>

        <div
            x-show=""isVisible""
            x-transition
            @click=""hidePopup()""
            class=""relative min-h-screen flex items-center justify-center p-4""
        >
            <div
                x-on:click.stop
                x-trap.noscroll.inert=""isVisible""
                class=""relative max-w-xl w-full bg-white rounded-lg shadow-lg p-4 overflow-y-auto""
            >
                <div class=""flex items-center justify-between"">
                    <div class=""flex-1 min-w-0"">&nbsp;</div>
                    <div class=""flex mt-0 ml-4"">
                        <button
                            type=""button""
                            class=""rounded-md text-gray-500 hover:text-gray-400 px-2 focus:outline-none focus:ring-0""
                            @click=""hidePopup()""
                        >
                            <span class=""sr-only"">Zamknij</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times"" class=""svg-inline--fa fa-times fa-lg"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512""><path fill=""currentColor"" d=""M193.94 256L296.5 153.44l21.15-21.15c3.12-3.12 3.12-8.19 0-11.31l-22.63-22.63c-3.12-3.12-8.19-3.12-11.31 0L160 222.06 36.29 98.34c-3.12-3.12-8.19-3.12-11.31 0L2.34 120.97c-3.12 3.12-3.12 8.19 0 11.31L126.06 256 2.34 379.71c-3.12 3.12-3.12 8.19 0 11.31l22.63 22.63c3.12 3.12 8.19 3.12 11.31 0L160 289.94 262.56 392.5l21.15 21.15c3.12 3.12 8.19 3.12 11.31 0l22.63-22.63c3.12-3.12 3.12-8.19 0-11.31L193.94 256z""></path></svg>    

                        </button>
                    </div>
                </div>

                <div class=""sm:px-4"">
                    <div class=""sm:px-6 space-y-2"">
                        <h3 class=""font-semibold text-primary"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" class=""svg-inline--fa  text-primary mr-1 rotate-12"" style="""" viewBox=""0 0 448 512""><path fill=""currentColor"" d=""M207.1 16C207.1 7.164 215.2 0 223.1 0C232.8 0 240 7.164 240 16V32.79C320.9 40.82 384 109 384 192V221.1C384 264.8 401.4 306.7 432.3 337.7L435 340.4C443.3 348.7 448 359.1 448 371.7C448 396.2 428.2 416 403.7 416H44.28C19.83 416 0 396.2 0 371.7C0 359.1 4.666 348.7 12.97 340.4L15.72 337.7C46.63 306.7 64 264.8 64 221.1V192C64 109 127.1 40.82 208 32.79L207.1 16zM223.1 64C153.3 64 95.1 121.3 95.1 192V221.1C95.1 273.3 75.26 323.4 38.35 360.3L35.6 363C33.29 365.3 31.1 368.5 31.1 371.7C31.1 378.5 37.5 384 44.28 384H403.7C410.5 384 416 378.5 416 371.7C416 368.5 414.7 365.3 412.4 363L409.7 360.3C372.7 323.4 352 273.3 352 221.1V192C352 121.3 294.7 64 223.1 64H223.1zM223.1 480C237.9 480 249.8 471.1 254.2 458.7C257.1 450.3 266.3 445.1 274.6 448.9C282.9 451.9 287.3 461 284.4 469.3C275.6 494.2 251.9 512 223.1 512C196.1 512 172.4 494.2 163.6 469.3C160.7 461 165.1 451.9 173.4 448.9C181.7 445.1 190.9 450.3 193.8 458.7C198.2 471.1 210.1 480 223.1 480z""/></svg>    
Praca alert - powiadomienia e-mail
                        </h3>

                        <span class=""inline-flex items-center rounded-full bg-gray-100 px-2.5 py-1.5 text-sm font-semibold text-gray-800"">
                            Magazynier, Sosnowiec
                        </span>
                        <p class=""text-xs sm:text-sm text-gray-700"">
                            Wyślemy Ci na adres e-mail powiadomienia o nowych ofertach pracy!
                        </p>

                        <div>
                            <input
                                type=""email""
                                placeholder=""Wpisz adres e-mail""
                                maxlength=""100""
                                x-model.debounce=""subscriberEmail""
                                @keypress=""validationEmailErrorVisible = false""
                                @keyup.enter=""subscribe()""
                                class=""w-full rounded-md border-gray-300 shadow-sm placeholder:text-gray-500 text-sm sm:text-base focus:outline-none focus:ring-0 focus:ring-offset-0""
                            >
                        </div>

                        <div class=""flex"">
                            <input
                                id=""job-alert-consent""
                                type=""checkbox""
                                class=""form-check-input cursor-pointer""
                                x-model=""subscriberEmailConsent""
                                @click=""validationEmailErrorVisible = false""
                            >
                            <div class=""ml-1 sm:mt-1 text-xs"" x-data=""{ showMore: false }"">
                                <label for=""job-alert-consent"" class=""text-gray-600 cursor-pointer"">
                                    Zgadzam się na otrzymywanie wiadomości e-mail z&nbsp;nowymi ofertami.

                                    <button class=""font-medium text-primary"" @click=""showMore = !showMore"">
                                        <span x-text=""showMore ? 'Mniej' : 'Więcej'""></span>
                                    </button>
                                    <div x-show=""showMore"" class=""fade-in mt-1"">
                                        <p class=""opacity-100 leading-3 text-gray-500"">
                                            Wyrażam zgodę na przesyłanie mi przez Aplikuj.pl informacji handlowych za pomocą środków komunikacji elektronicznej, w tym w szczególności poczty elektronicznej (wiadomości e-mail), zawierających powiadomienia o nowych ofertach pracy na portalu Aplikuj.pl zgodnie z wybranymi przeze mnie kryteriami wyszukiwania.<br>
                                            W każdej chwili możesz anulować subskrypcję powiadomień klikając w link z rezygnacją w wiadomości e-mail.
                                        </p>
                                    </div>
                                </label>
                            </div>
                        </div>

                        <div class=""h-4"">
                            <div
                                class=""text-xs text-red-600 ""
                                x-show=""validationEmailErrorVisible""
                                x-transition:enter=""transition ease-out duration-100""
                                x-transition:enter-start=""opacity-0 scale-90""
                                x-transition:enter-end=""opacity-100 scale-100""
                            >
                                Wpisz poprawny adres e-mail i zaakceptuj zgodę.
                            </div>
                        </div>
                    </div>



                    <div class=""mt-4 sm:mt-6 p-2 sm:p-6 space-y-2 bg-indigo-50 rounded-lg"">
                        <h3 class=""font-semibold text-primary"">
                                    
            <svg xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512"" class=""svg-inline--fa  text-primary mr-2"" style=""""><path fill=""currentColor"" d=""M192 416c0 17.7-14.3 32-32 32s-32-14.3-32-32 14.3-32 32-32 32 14.3 32 32zM320 48v416c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V48C0 21.5 21.5 0 48 0h224c26.5 0 48 21.5 48 48zm-32 0c0-8.8-7.2-16-16-16H48c-8.8 0-16 7.2-16 16v416c0 8.8 7.2 16 16 16h224c8.8 0 16-7.2 16-16V48z""/></svg>    
Praca na SMS - <span class=""font-normal text-base sm:text-lg"">oferty specjalne</span>
                        </h3>

                        <p class=""text-xs sm:text-sm text-gray-600"">
                            Zostaw nam swój numer, aby otrzymywać specjalne oferty na SMS.
                        </p>

                        <div class=""flex bg-white rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-1 focus-within:ring-inset focus-within:ring-indigo-600"">
                            <span class=""flex select-none items-center pl-3 text-gray-500 text-sm sm:text-base"">+48</span>
                            <input
                                type=""text""
                                class=""appearance-none block flex-1 border-0 bg-transparent pl-1 text-gray-900 placeholder:text-gray-500 focus:ring-0 text-sm sm:text-base sm:leading-6""
                                placeholder=""Wpisz nr telefonu komórkowego""
                                maxlength=""9""
                                x-model.debounce=""subscriberPhone""
                                @keypress=""validationPhoneErrorVisible = false""
                            >
                        </div>

                        <div class=""flex"">
                            <input
                                id=""sms-alert-consent""
                                type=""checkbox""
                                class=""form-check-input cursor-pointer""
                                x-model=""subscriberPhoneConsent""
                                @click=""validationPhoneErrorVisible = false""
                            >
                            <div class=""ml-2 text-xs"" x-data=""{ showMore: false }"">
                                <label for=""sms-alert-consent"" class=""text-gray-500 cursor-pointer text-left"">
                                    Zgadzam się na otrzymywanie powiadomień dotyczących wyselekcjonowanych ofert pracy w&nbsp;formie wiadomości SMS.
                                    <button class=""font-medium text-primary"" @click=""showMore = !showMore"">
                                        <span x-text=""showMore ? 'Mniej' : 'Więcej'""></span>
                                    </button>
                                    <div x-show=""showMore"" class=""fade-in mt-1"">
                                        <p class=""opacity-100 leading-3 text-gray-500"">
                                            Wyrażam zgodę na wykorzystywanie przez Aplikuj.pl mojego numeru telefonu w celu przesyłania mi za pomocą wiadomości tekstowych (SMS) wiadomości dotyczących wyselekcjonowanych ofert pracy na podany przeze mnie w procesie dodawania powiadomienia numer telefonu.<br>
                                            W każdej chwili możesz anulować subskrypcję powiadomień klikając <a href=""/powiadomienia/zarzadzaj"" class=""text-primary sm:hover:underline"" rel=""nofollow"" target=""_blank"">tutaj</a>.
                                        </p>
                                    </div>
                                </label>
                            </div>
                        </div>

                        <div class=""h-4"">
                            <div
                                class=""text-xs text-red-600 ""
                                x-show=""validationPhoneErrorVisible""
                                x-transition:enter=""transition ease-out duration-100""
                                x-transition:enter-start=""opacity-0 scale-90""
                                x-transition:enter-end=""opacity-100 scale-100""
                            >
                                Wpisz poprawny numer telefonu i zaakceptuj zgodę.
                            </div>
                        </div>
                    </div>

                    <div class=""flex justify-center mt-6"">
                        <button
                            class=""btn btn-primary sm:text-lg sm:py-2 sm:px-4 whitespace-nowrap""
                            type=""button""
                            @click=""subscribe()""
                        >
                            Zapisz się
                        </button>
                    </div>

                </div>

                <div class=""text-center text-xs text-gray-500 space-y-0.5 pb-2 mt-6"">
                    <p class=""font-semibold"">PAMIĘTAJ, żeby odebrać wiadomość potwierdzającą i&nbsp;kliknąć w&nbsp;niej w&nbsp;przycisk.</p>
                    <p>Jeśli posiadasz już konto kandydata, <a class=""text-primary"" :href=""loginUrl"">zaloguj się</a></p>
                </div>
            </div>
        </div>
    </div>
</div>
            
                        

                
        
        <script>
                            dataLayer.push({
                  'event':'jobOfferShow',
                  'jobId': '2960357',
                  'jobWorkPlace': 'Sosnowiec',
                  'jobPageType': 'offerDetail',
                  'jobOfferValue':         
    0.05

                });
            
                                                                                                
            
                                                                                                
            
            
                            dataLayer.push({
                  'event':'jobOfferShow',
                  'jobOfferType': 'ih-p-r',
                  'jobOfferValue':         
    0.05
,
                });
            
            function externalApplyHandleClick(url) {
                ExternalApplyService.send('db146d26-8c67-4dd3-b00c-3361fe7179ef').then(response => {
                    window.location.href = url;
                }).catch(error => {
                    console.error('External apply handle click', error);
                });

                                    dataLayer.push({
                      'event':'jobApply',
                      'applyType': 'ih-p-r',
                      'jobOfferValue':         
    0.05
,
                    });
                            }

            function userApplyAtsHandleClick(url) {
                UserApplyAts.send('db146d26-8c67-4dd3-b00c-3361fe7179ef').then(response => {
                  window.location.href = url;
                }).catch(error => {
                  console.error('Internal apply handle click', error);
                });

                                  dataLayer.push({
                    'event':'jobApply',
                    'applyType': 'ih-p-r',
                    'jobOfferValue':         
    0.05
,
                  });
                            }

                    </script>






<noscript><iframe src=""https://www.googletagmanager.com/ns.html?id=GTM-WVK5S78"" height=""0"" width=""0"" style=""display:none;visibility:hidden""></iframe></noscript>
<noscript><img height=""1"" width=""1"" style=""display:none"" src=""https://www.facebook.com/tr?id=303246673786053&ev=PageView&noscript=1"" alt=""""></noscript>

<script>
window.onload = () => {
    RefreshUserSession();
};
</script>
<div
    x-cloak
    x-data=""signInCandidate""
    x-bind=""eventListeners""
    x-init=""initialize('https://www.aplikuj.pl/kandydat', '/user/sign-in-success')""
>
    <div
        x-show=""isVisible""
        @keydown.escape.prevent.stop=""hidePopup()""
        role=""dialog""
        aria-modal=""true""
        class=""fixed inset-0 overflow-y-auto z-100 sm:z-50""
    >
        <div
            x-show=""isVisible""
            x-transition.opacity
            class=""modal-overlay""
            aria-hidden=""true""
        ></div>

        <div
            x-show=""isVisible""
            x-transition
            @click=""hidePopup()""
            class=""relative w-full min-h-screen flex items-center justify-center p-0 sm:p-4""
        >
            <div
                x-on:click.stop
                x-trap.noscroll.inert=""isVisible""
                class=""relative w-full h-screen sm:h-auto sm:w-2/3 xl:w-112 bg-white sm:rounded-lg sm:shadow-lg p-4 overflow-y-auto""
            >
                <div class=""flex items-center justify-between"">
                    <div class=""flex-1 min-w-0""></div>
                    <div class=""flex mt-0 ml-4"">
                        <button
                            type=""button""
                            class=""rounded-md text-gray-500 hover:text-gray-400 px-2 focus:outline-none focus:ring-0""
                            @click=""hidePopup()""
                        >
                            <span class=""sr-only"">Zamknij</span>
                                    
            <svg aria-hidden=""true"" focusable=""false"" data-prefix=""fal"" data-icon=""times"" class=""svg-inline--fa fa-times fa-lg"" style="""" role=""img"" xmlns=""https://www.w3.org/2000/svg"" viewBox=""0 0 320 512""><path fill=""currentColor"" d=""M193.94 256L296.5 153.44l21.15-21.15c3.12-3.12 3.12-8.19 0-11.31l-22.63-22.63c-3.12-3.12-8.19-3.12-11.31 0L160 222.06 36.29 98.34c-3.12-3.12-8.19-3.12-11.31 0L2.34 120.97c-3.12 3.12-3.12 8.19 0 11.31L126.06 256 2.34 379.71c-3.12 3.12-3.12 8.19 0 11.31l22.63 22.63c3.12 3.12 8.19 3.12 11.31 0L160 289.94 262.56 392.5l21.15 21.15c3.12 3.12 8.19 3.12 11.31 0l22.63-22.63c3.12-3.12 3.12-8.19 0-11.31L193.94 256z""></path></svg>    

                        </button>
                    </div>
                </div>

                <div class=""sm:px-4"">
                    <h3
                        class=""font-semibold mb-2 text-primary""
                        x-text=""popupTitle""
                        x-show=""popupTitle""
                    ></h3>
                    <div x-html=""iframeHtml""></div>
                    <div x-html=""additionalData""></div>
                </div>
            </div>
        </div>
    </div>
</div>



<div x-data=""eventLoggerService"" x-bind=""eventListeners"" class=""hidden""></div>

<script>
  (function () {
    let alreadyLoaded = false;

    function loadLazyScripts() {
      if (alreadyLoaded) return;
      alreadyLoaded = true;

      const scriptsToLoad = window.lazyScriptsToLoad || [];

      for (const scriptDef of scriptsToLoad) {
        if (scriptDef.inline && typeof scriptDef.code === 'function') {
          scriptDef.code();
        } else if (scriptDef.url) {
          const s = document.createElement('script');
          s.src = scriptDef.url;
          s.async = true;

          if (scriptDef.admitadFallback) {
            s.onerror = function () {
              const self = s;
              window.ADMITAD = window.ADMITAD || {};
              ADMITAD.Helpers = ADMITAD.Helpers || {};
              ADMITAD.Helpers.generateDomains = function () {const e = new Date();const n = Math.floor(new Date(2020, e.getMonth(), e.getDate()).setUTCHours(0, 0, 0, 0) / 1e3);const t = parseInt(1e12 * (Math.sin(n) + 1)).toString(30);const i = ['de'], o = [];for (let a = 0; a < i.length; ++a) o.push({ domain: t + '.' + i[a], name: t });return o;};
              ADMITAD.Helpers.findTodaysDomain = function (callback) {let t = 0;const domains = ADMITAD.Helpers.generateDomains();function tryLoad() {const x = new XMLHttpRequest();const current = domains[t].domain;const url = 'https://' + current + '/';x.open('HEAD', url, true);x.onload = function () {setTimeout(() => callback(domains[t]), 0);};x.onerror = function () {++t < domains.length ? setTimeout(tryLoad, 0) : setTimeout(() => callback(undefined), 0);};x.send();}tryLoad();};
              ADMITAD.Helpers.findTodaysDomain(function (domainData) {if (!domainData) return;const campaignCode = (/campaign_code=([^&]+)/.exec(self.src) || [])[1] || '';self.parentNode.removeChild(self);const s = document.createElement('script');s.src = 'https://' + domainData.domain + '/static/' + domainData.name.slice(1) + domainData.name.slice(0, 1) + '.min.js?campaign_code=' + campaignCode;document.head.appendChild(s);});
            };
          }

          document.head.appendChild(s);
        }
      }
    }

    function initLazyLoader() {
      if ('requestIdleCallback' in window) {
        requestIdleCallback(loadLazyScripts, { timeout: 1000 });
      } else {
        setTimeout(loadLazyScripts, 1000);
      }
    }

    ['scroll', 'mousemove', 'touchmove', 'click'].forEach(event => {
      window.addEventListener(event, initLazyLoader, { once: true, passive: true });
    });
  })();
</script>

</body>
</html>




            ";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-container\"]");

            OfferDetails det = new OfferDetails();
            det.dates = GetDates(node);
            det.company = GetCompany(node);
            HtmlNode detailsSection = node.SelectSingleNode(".//div[contains(@class, 'pt-6')]");
            HtmlNode informationSection = node.SelectSingleNode(".//div[contains(@class, 'pt-8')]");
            HtmlNodeCollection skillsSections = node.SelectNodes(".//div[contains(@class, 'pb-4 sm:pb-12')]");

            HtmlNode? salarySection = node.SelectSingleNode(".//div[contains(@class, 'flex bg-gray-100 rounded-lg py-1 lg:py-2.5 px-2 lg:px-4 mt-4')]");

            det.responsibilities = GetFeature(skillsSections.FirstOrDefault());
            if (skillsSections.Count > 1)
                det.requirements = GetFeature(skillsSections.ElementAt(1));
            if (skillsSections.Count > 2)
                det.benefits = GetFeature(skillsSections.ElementAt(2));
            det.localization = GetLocalization(informationSection, doc.DocumentNode);
            if (salarySection != null)
                det.salary = GetSalary(salarySection);
            det.infoFeatures = GetInfofeature(node);
            det.category = informationSection.SelectSingleNode(".//div//div[2]//div//div[2]//p").InnerText.Trim();

            return det;
        }

        private InfoFeatures GetInfofeature(HtmlNode document)
        {
            HtmlNode? infoSection = document.SelectSingleNode(".//div[contains(@class, 'grid grid-col-1 sm:grid-cols-2 gap-x-8 gap-y-3 py-3 md:pb-8 offer__label-wrapper')]");
            HtmlNodeCollection descriptionSection = document.SelectNodes(".//div[contains(@class, 'leading-6 py-6')]");
            InfoFeatures features = new InfoFeatures();
            string typeofContract = infoSection.SelectSingleNode(".//div//div[2]//p").InnerText.Trim();
            string typeofWork = infoSection.SelectSingleNode(".//div[2]//div[2]//p").InnerText.Trim();

            features.typeofContract = typeofContract;
            features.typeofWork = typeofWork;
           
            features.isRemote = false;
            foreach (HtmlNode node in infoSection.SelectNodes(".//div[contains(@class, 'flex-1')]"))
            {
                string? content = node.SelectSingleNode(".//p")?.InnerText.Trim();
                if (content != null && content.Contains("Praca zdalna"))
                    features.isRemote = true; 
                if (content != null && content.Contains("Запрошуємо працівників з України"))
                    features.isforUkrainians = true;
            }
            features.description = string.Join("", descriptionSection.Select(n => n.InnerHtml)).Trim();

            return features;
        }
        AplikujPl.Salary GetSalary(HtmlNode node)
        {
            AplikujPl.Salary salaryObj = new AplikujPl.Salary();
            HtmlNode salaryBlock = node.SelectSingleNode(".//ul//li//div");
            string typeofContract = salaryBlock.SelectSingleNode(".//span").InnerText.Trim();
            string salary = salaryBlock.SelectSingleNode(".//div//span").InnerText.Trim();

            return SalaryParser.Parse(salary);
        }
        Localization GetLocalization(HtmlNode? node, HtmlNode? allNodes)
        {
            Localization loc = new Localization();
            HtmlNode? tempNode = node?.SelectSingleNode(".//li[contains(@class, 'pt-1')]");
            string localizatinoString = tempNode?.SelectSingleNode(".//div[2]//span//span").InnerText.Trim() ?? "";
            loc.city = localizatinoString.Split(',')[0];
            return loc;
        }
        List<string> GetFeature(HtmlNode? node)
        {
            List<string> featureList = new List<string>();
            HtmlNodeCollection? skills = node?.SelectNodes(".//li");
            HtmlNodeCollection? skills2 = node?.SelectNodes(".//p");

            if (skills != null)
            {
                foreach (HtmlNode respo in skills)
                {
                    featureList.Add(respo.SelectNodes(".//div").ElementAt(1)?.InnerText.Trim() ?? "");
                }
                return featureList;
            }
            else if (skills2 != null && skills == null)
            {
                foreach (HtmlNode respo in skills2.Skip(1))
                {
                    featureList.Add(respo.InnerText.Trim() ?? "");
                }
                return featureList;
            }
            return new List<string>();
        }
    }
}
