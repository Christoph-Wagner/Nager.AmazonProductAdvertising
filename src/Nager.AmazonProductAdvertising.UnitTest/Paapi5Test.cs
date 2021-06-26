﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.AmazonProductAdvertising.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Nager.AmazonProductAdvertising.UnitTest
{
    [Ignore]
    [TestClass]
    public class Paapi5Test
    {
        private AmazonProductAdvertisingClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            var accessKey = "";
            var secretKey = "";
            var parnterTag = "";
            var endpoint = AmazonEndpoint.DE;

            var amazonAuthentication = new AmazonAuthentication(accessKey, secretKey);
            this._client = new AmazonProductAdvertisingClient(amazonAuthentication, endpoint, parnterTag, strictJsonMapping: true);
        }

        [DataTestMethod]
        [DataRow("Auto")]
        [DataRow("Kleider")]
        [DataRow("Schuhe")]
        [DataRow("Staubsauger")]
        [DataRow("Teller")]
        [DataRow("Sonnenbrille")]
        [DataRow("Wasserkocher")]
        [DataRow("Nussknacker")]
        public async Task SearchItemsTest1(string keyword)
        {
            var response = await this._client.SearchItemsAsync(keyword);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.SearchResult.Items.Length);
        }

        [DataTestMethod]
        [DataRow("Harry Potter")]
        public async Task SearchItemsWithSearchIndex(string keyword)
        {
            var responseDefaultSearch = await this._client.SearchItemsAsync(new SearchRequest
            {
                Keywords = keyword,
                Resources = new []
                {
                    "ItemInfo.Title",
                },
            });

            var responseWithSearchIndex = await this._client.SearchItemsAsync(new SearchRequest
            {
                Keywords = keyword,
                SearchIndex = SearchIndex.Books,
                Resources = new []
                {
                    "ItemInfo.Title",
                },
            });

            Assert.IsTrue(responseDefaultSearch.Successful);
            Assert.IsTrue(responseWithSearchIndex.Successful);

            var asins1 = responseDefaultSearch.SearchResult?.Items.Select(o => o.ASIN).ToArray();
            var asins2 = responseWithSearchIndex.SearchResult?.Items.Select(o => o.ASIN).ToArray();

            CollectionAssert.AreNotEqual(asins1, asins2);
        }

        [DataTestMethod]
        [DataRow("aefsefwdfd")]
        [DataRow("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
        [DataRow("")]
        [DataRow(null)]
        public async Task SearchItemsTest2(string keyword)
        {
            var response = await this._client.SearchItemsAsync(keyword);
            //Assert.IsTrue(response.Successful);
            //Assert.AreEqual(10, response.SearchResult.Items.Length);
        }

        [TestMethod]

        public async Task SearchItemsTest3()
        {
            var request = new SearchRequest
            {
                BrowseNodeId = "84230031",
                SortBy = SortBy.AvgCustomerReviews,
                Keywords = "*",
                Resources = new []
                {
                    "Images.Primary.Small",
                    "ItemInfo.Title",
                },
            };

            var response = await this._client.SearchItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.SearchResult.Items.Length);
        }

        [TestMethod]
        public async Task SearchItemsTest4()
        {
            var request = new SearchRequest
            {
                SortBy = SortBy.AvgCustomerReviews,
                Keywords = "iPhone",
                Resources = new []
                {
                    "Images.Primary.Small",
                    "ItemInfo.Title",
                },
                Merchant = Merchant.All
            };

            var response = await this._client.SearchItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.SearchResult.Items.Length);
        }

        [TestMethod]
        public async Task SearchItemsTest5()
        {
            var request = new SearchRequest
            {
                Keywords = "iPhone",
                Resources = new[]
                {
                    "SearchRefinements"
                }
            };

            var response = await this._client.SearchItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.SearchResult.Items.Length);
        }

        [TestMethod]
        public async Task SearchItemsTest6()
        {
            var request = new SearchRequest
            {
                SearchIndex = SearchIndex.Books,
                Resources = new[]
                {
                    "BrowseNodeInfo.BrowseNodes",
                    "BrowseNodeInfo.BrowseNodes.Ancestor",
                    "BrowseNodeInfo.BrowseNodes.SalesRank",
                    "Images.Primary.Small",
                    "Images.Primary.Medium",
                    "Images.Primary.Large",

                    "Images.Variants.Small",
                    "Images.Variants.Medium",
                    "Images.Variants.Large",

                    "ItemInfo.ByLineInfo",
                    "ItemInfo.Classifications",
                    "ItemInfo.ContentInfo",
                    "ItemInfo.ContentRating",
                    "ItemInfo.ExternalIds",
                    "ItemInfo.Features",
                    "ItemInfo.ProductInfo",
                    "ItemInfo.TechnicalInfo",
                    "ItemInfo.Title",
                    "ItemInfo.TradeInInfo",

                    "Offers.Listings.Availability.MinOrderQuantity",
                    "Offers.Listings.Availability.MaxOrderQuantity",
                    "Offers.Listings.Availability.Message",
                    "Offers.Listings.Availability.Type",
                    "Offers.Listings.Condition",
                    "Offers.Listings.Condition.SubCondition",
                    "Offers.Listings.DeliveryInfo.IsAmazonFulfilled",
                    "Offers.Listings.DeliveryInfo.IsFreeShippingEligible",
                    "Offers.Listings.DeliveryInfo.IsPrimeEligible",
                    "Offers.Listings.IsBuyBoxWinner",
                    "Offers.Listings.LoyaltyPoints.Points",
                    "Offers.Listings.MerchantInfo",
                    "Offers.Listings.Price",
                    "Offers.Listings.ProgramEligibility.IsPrimeExclusive",
                    "Offers.Listings.ProgramEligibility.IsPrimePantry",
                    "Offers.Listings.Promotions",
                    "Offers.Listings.SavingBasis",
                    "Offers.Summaries.HighestPrice",
                    "Offers.Summaries.LowestPrice",
                    "Offers.Summaries.OfferCount",

                    "ParentASIN",
                    "SearchRefinements"
                }
            };

            var response = await this._client.SearchItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.SearchResult.Items.Length);
        }

        [TestMethod]
        public async Task SearchItemsTest7()
        {
            var request = new SearchRequest
            {
                SearchIndex = SearchIndex.Books,
                Resources = new[]
                {
                    "Offers.Listings.IsBuyBoxWinner",
                }
            };

            var response = await this._client.SearchItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.SearchResult.Items.Length);
            Assert.IsTrue(response.SearchResult.Items.Any(item => item.Offers.Listings.Any(l => l.IsBuyBoxWinner)));
        }


        [TestMethod]
        public async Task GetBrowseNodesAsyncTest1()
        {
            var request = new BrowseNodesRequest
            {
                BrowseNodeIds = new[] { "1981019031" },
                Resources = new[] { "BrowseNodes.Ancestor", "BrowseNodes.Children" },
                LanguagesOfPreference = new[] { "de_DE" }
            };

            var response = await this._client.GetBrowseNodesAsync(request);
            Assert.IsTrue(response.Successful);
        }

        [DataTestMethod]
        [DataRow("B00IYD5QUO")] //shower gel
        [DataRow("B07CJPSNX8")] //clothes, dress
        [DataRow("3551317267")] //book, harry potter
        [DataRow("B00BQ6RS7K")] //food, sweets
        [DataRow("B01LFB3R0W")] //electronic, cable
        public async Task GetItemsTest(string asin)
        {
            var response = await this._client.GetItemsAsync(asin);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(1, response.ItemsResult.Items.Length);
        }

        [TestMethod]
        public async Task GetVariationsTest()
        {
            var response = await this._client.GetVariationsAsync("B07QP2SF9J");
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(10, response.VariationsResult.Items.Length);
        }

        [TestMethod]
        public async Task GetItemsWithConditionTest()
        {
            var request = new ItemsRequest
            {
                ItemIds = new string[] { "B000NM8SEA" },
                Condition = Condition.Used,
                Resources = new []
                {
                    "Offers.Listings.Condition",
                    "Offers.Listings.Price"
                }
            };

            var response = await this._client.GetItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.AreEqual(Condition.Used.ToString(), response.ItemsResult.Items[0].Offers.Listings[0].Condition.Value);
        }

        [TestMethod]
        public async Task WebsiteSalesRankTest()
        {
            var request = new SearchRequest
            {
                Keywords = "9780060004873",
                SearchIndex = SearchIndex.All,
                Resources = new []
                {
                    "Images.Primary.Medium",
                    "BrowseNodeInfo.BrowseNodes",
                    "BrowseNodeInfo.WebsiteSalesRank",
                    "ItemInfo.ExternalIds",
                    "ItemInfo.Title",
                    "ItemInfo.ByLineInfo",
                    "ItemInfo.ManufactureInfo",
                    "ItemInfo.ProductInfo",
                    "ItemInfo.Classifications",
                    "Offers.Listings.Price",
                    "Offers.Listings.MerchantInfo"
                }
            };

            var response = await this._client.SearchItemsAsync(request);
            Assert.IsTrue(response.Successful);
            Assert.IsNotNull(response.SearchResult.Items[0].BrowseNodeInfo.WebsiteSalesRank);
        }
    }
}
