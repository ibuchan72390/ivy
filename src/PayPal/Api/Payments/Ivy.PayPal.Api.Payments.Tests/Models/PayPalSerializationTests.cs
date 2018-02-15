using Ivy.IoC;
using Ivy.PayPal.Api.Payments.Core.Models.Response;
using Ivy.PayPal.Api.Payments.Tests.Base;
using Ivy.Web.Core.Json;
using Xunit;

namespace Ivy.PayPal.Api.Payments.Tests.Models
{
    public class PayPalSerializationTests : PayPalPaymentsTestBase
    {
        #region Variables & Constants

        private readonly IJsonSerializationService _serializer;
       
        #endregion

        #region SetUp & TearDown

        public PayPalSerializationTests()
        {
            _serializer = ServiceLocator.Instance.GetService<IJsonSerializationService>();
        }

        #endregion

        #region Tests

        [Fact]
        public void PayPal_CreatePayment_Response_Serialization_Test()
        {
            /*
             * This was pulled directly from PayPal documentation at the following url:
             * https://developer.paypal.com/docs/api/payments/#payment_list
             */
            const string json = @"{
	            ""intent"": ""sale"",
	            ""payer"": {
		            ""payment_method"": ""paypal""
	            },
	            ""transactions"": [{
		            ""amount"": {
			            ""total"": ""30.11"",
			            ""currency"": ""USD"",
			            ""details"": {
				            ""subtotal"": ""30.00"",
				            ""tax"": ""0.07"",
				            ""shipping"": ""0.03"",
				            ""handling_fee"": ""1.00"",
				            ""shipping_discount"": ""-1.00"",
				            ""insurance"": ""0.01""

                        }
                    },
		            ""description"": ""The payment transaction description."",
		            ""custom"": ""EBAY_EMS_90048630024435"",
		            ""invoice_number"": ""48787589673"",
		            ""payment_options"": {
			            ""allowed_payment_method"": ""INSTANT_FUNDING_SOURCE""
		            },
		            ""soft_descriptor"": ""ECHI5786786"",
		            ""item_list"": {
			            ""items"": [{
					            ""name"": ""hat"",
					            ""description"": ""Brown hat."",
					            ""quantity"": ""5"",
					            ""price"": ""3"",
					            ""tax"": ""0.01"",
					            ""sku"": ""1"",
					            ""currency"": ""USD""
				            },
				            {
					            ""name"": ""handbag"",
					            ""description"": ""Black handbag."",
					            ""quantity"": ""1"",
					            ""price"": ""15"",
					            ""tax"": ""0.02"",
					            ""sku"": ""product34"",
					            ""currency"": ""USD""
				            }
			            ],
			            ""shipping_address"": {
				            ""recipient_name"": ""Brian Robinson"",
				            ""line1"": ""4th Floor"",
				            ""line2"": ""Unit #34"",
				            ""city"": ""San Jose"",
				            ""country_code"": ""US"",
				            ""postal_code"": ""95131"",
				            ""phone"": ""011862212345678"",
				            ""state"": ""CA""
			            }
		            }
	            }],
	            ""note_to_payer"": ""Contact us for any questions on your order."",
	            ""redirect_urls"": {
		            ""return_url"": ""https://example.com/return"",
		            ""cancel_url"": ""https://example.com/cancel""
	            }
            }";


            var result = _serializer.Deserialize<PayPalPaymentCreateResponse>(json);

            Assert.NotNull(result);
            Assert.NotNull(result.payer);
            Assert.NotNull(result.transactions);
            Assert.NotNull(result.redirect_urls);

            foreach (var trans in result.transactions)
            {
                Assert.NotNull(trans.amount);
                Assert.NotNull(trans.amount.details);
                Assert.NotNull(trans.payment_options);
            }
        }

        [Fact]
        public void PayPal_ListPayment_Response_Serialization_Test()
        {
            /*
             * This was pulled directly from PayPal documentation at the following url:
             * https://developer.paypal.com/docs/api/payments/#payment_list
             */
            const string json = @"{
              ""payments"": [
                {
                            ""id"": ""PAY-0US81985GW1191216KOY7OXA"",
                  ""create_time"": ""2017-06-30T23:48:44Z"",
                  ""update_time"": ""2017-06-30T23:49:27Z"",
                  ""state"": ""approved"",
                  ""intent"": ""order"",
                  ""payer"": {
                                ""payment_method"": ""paypal""
                  },
                  ""transactions"": [
                    {
                      ""amount"": {
                        ""total"": ""41.15"",
                        ""currency"": ""USD"",
                        ""details"": {
                          ""subtotal"": ""30.00"",
                          ""tax"": ""1.15"",
                          ""shipping"": ""10.00""
                        }
                      },
                      ""description"": ""The payment transaction description."",
                      ""item_list"": {
                        ""items"": [
                          {
                            ""name"": ""hat"",
                            ""sku"": ""1"",
                            ""price"": ""3.00"",
                            ""currency"": ""USD"",
                            ""quantity"": ""5""
                          },
                          {
                            ""name"": ""handbag"",
                            ""sku"": ""product34"",
                            ""price"": ""15.00"",
                            ""currency"": ""USD"",
                            ""quantity"": ""1""
                          }
                        ],
                        ""shipping_address"": {
                          ""recipient_name"": ""John Doe"",
                          ""line1"": ""4th Floor, One Lagoon Drive"",
                          ""line2"": ""Unit #34"",
                          ""city"": ""Redwood City"",
                          ""state"": ""CA"",
                          ""phone"": ""4084217591"",
                          ""postal_code"": ""94065"",
                          ""country_code"": ""US""
                        }
                      },
                      ""related_resources"": [
                        {
                          ""authorization"": {
                            ""id"": ""53P09338XY5426455"",
                            ""create_time"": ""2017-06-30T23:50:01Z"",
                            ""update_time"": ""2017-06-30T23:50:01Z"",
                            ""amount"": {
                              ""total"": ""41.15"",
                              ""currency"": ""USD""
                            },
                            ""parent_payment"": ""PAY-0US81985GW1191216KOY7OXA"",
                            ""valid_until"": ""2017-07-29T23:49:52Z"",
                            ""links"": [
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-0US81985GW1191216KOY7OXA"",
                                ""rel"": ""parent_payment"",
                                ""method"": ""GET""
                              }
                            ]
                          }
                        }
                      ]
                    }
                  ],
                  ""links"": [
                    {
                      ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-0US81985GW1191216KOY7OXA"",
                      ""rel"": ""self"",
                      ""method"": ""GET""
                    }
                  ]
                },
                {
                  ""id"": ""PAY-53485002LD6169910KOZQ25I"",
                  ""create_time"": ""2017-07-01T19:35:17Z"",
                  ""update_time"": ""2017-07-01T19:36:05Z"",
                  ""state"": ""approved"",
                  ""intent"": ""order"",
                  ""payer"": {
                    ""payment_method"": ""paypal""
                  },
                  ""transactions"": [
                    {
                      ""amount"": {
                        ""total"": ""33.00"",
                        ""currency"": ""USD"",
                        ""details"": {
                          ""subtotal"": ""21.00"",
                          ""tax"": ""2.00"",
                          ""shipping"": ""10.00""
                        }
                      },
                      ""description"": ""The payment transaction description."",
                      ""item_list"": {
                        ""items"": [
                          {
                            ""name"": ""hat"",
                            ""sku"": ""1"",
                            ""price"": ""3.00"",
                            ""currency"": ""USD"",
                            ""quantity"": ""2""
                          },
                          {
                            ""name"": ""handbag"",
                            ""sku"": ""product34"",
                            ""price"": ""15.00"",
                            ""currency"": ""USD"",
                            ""quantity"": ""1""
                          }
                        ],
                        ""shipping_address"": {
                          ""recipient_name"": ""Hannah Lu"",
                          ""line1"": ""1602 Crane ct"",
                          ""line2"": """",
                          ""city"": ""San Jose"",
                          ""state"": ""CA"",
                          ""phone"": ""4084217591"",
                          ""postal_code"": ""95052"",
                          ""country_code"": ""US""
                        }
                      },
                      ""related_resources"": [
                        {
                          ""authorization"": {
                            ""id"": ""91527087GH224122L"",
                            ""create_time"": ""2017-07-01T19:36:22Z"",
                            ""update_time"": ""2017-07-01T19:36:22Z"",
                            ""amount"": {
                              ""total"": ""33.00"",
                              ""currency"": ""USD""
                            },
                            ""parent_payment"": ""PAY-53485002LD6169910KOZQ25I"",
                            ""valid_until"": ""2017-07-30T19:36:22Z"",
                            ""links"": [
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-53485002LD6169910KOZQ25I"",
                                ""rel"": ""parent_payment"",
                                ""method"": ""GET""
                              }
                            ]
                          }
                        }
                      ]
                    }
                  ],
                  ""links"": [
                    {
                      ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-53485002LD6169910KOZQ25I"",
                      ""rel"": ""self"",
                      ""method"": ""GET""
                    }
                  ]
                },
                {
                  ""id"": ""PAY-7F5790198P134484LKOZSG7Q"",
                  ""create_time"": ""2017-07-01T21:09:18Z"",
                  ""update_time"": ""2017-07-01T22:31:56Z"",
                  ""state"": ""approved"",
                  ""intent"": ""order"",
                  ""payer"": {
                    ""payment_method"": ""paypal""
                  },
                  ""transactions"": [
                    {
                      ""amount"": {
                        ""total"": ""42.00"",
                        ""currency"": ""USD"",
                        ""details"": {
                          ""subtotal"": ""36.00"",
                          ""tax"": ""1.00"",
                          ""shipping"": ""5.00""
                        }
                      },
                      ""description"": ""The payment transaction description."",
                      ""item_list"": {
                        ""items"": [
                          {
                            ""name"": ""handbag"",
                            ""sku"": ""product34"",
                            ""price"": ""36.00"",
                            ""currency"": ""USD"",
                            ""quantity"": ""1""
                          }
                        ],
                        ""shipping_address"": {
                          ""recipient_name"": ""Anna Joseph"",
                          ""line1"": ""2525 North 1st street"",
                          ""line2"": ""unit 4"",
                          ""city"": ""San Jose"",
                          ""state"": ""CA"",
                          ""phone"": ""011862212345678"",
                          ""postal_code"": ""95031"",
                          ""country_code"": ""US""
                        }
                      },
                      ""related_resources"": [
                        {
                          ""capture"": {
                            ""id"": ""26062838D7499294V"",
                            ""create_time"": ""2017-07-01T21:16:22Z"",
                            ""update_time"": ""2017-07-01T21:16:24Z"",
                            ""amount"": {
                              ""total"": ""7.00"",
                              ""currency"": ""USD""
                            },
                            ""state"": ""completed"",
                            ""parent_payment"": ""PAY-7F5790198P134484LKOZSG7Q"",
                            ""links"": [
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/capture/26062838D7499294V"",
                                ""rel"": ""self"",
                                ""method"": ""GET""
                              },
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/capture/26062838D7499294V/refund"",
                                ""rel"": ""refund"",
                                ""method"": ""POST""
                              },
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-7F5790198P134484LKOZSG7Q"",
                                ""rel"": ""parent_payment"",
                                ""method"": ""GET""
                              }
                            ]
                          }
                        },
                        {
                          ""capture"": {
                            ""id"": ""0YU20012P1477553M"",
                            ""create_time"": ""2017-07-01T22:31:54Z"",
                            ""update_time"": ""2017-07-01T22:31:56Z"",
                            ""amount"": {
                              ""total"": ""35.00"",
                              ""currency"": ""USD""
                            },
                            ""state"": ""completed"",
                            ""parent_payment"": ""PAY-7F5790198P134484LKOZSG7Q"",
                            ""links"": [
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/capture/0YU20012P1477553M"",
                                ""rel"": ""self"",
                                ""method"": ""GET""
                              },
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/capture/0YU20012P1477553M/refund"",
                                ""rel"": ""refund"",
                                ""method"": ""POST""
                              },
                              {
                                ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-7F5790198P134484LKOZSG7Q"",
                                ""rel"": ""parent_payment"",
                                ""method"": ""GET""
                              }
                            ]
                          }
                        }
                      ]
                    }
                  ],
                  ""links"": [
                    {
                      ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-7F5790198P134484LKOZSG7Q"",
                      ""rel"": ""self"",
                      ""method"": ""GET""
                    }
                  ]
                }
              ],
              ""count"": 3,
              ""next_id"": ""PAY-9X4935091L753623RKOZTRHI""
            }";


            var result = _serializer.Deserialize<PayPalPaymentListResponse>(json);

            Assert.NotNull(result);
            Assert.True(result.payments.Count > 0);
            Assert.True(result.count == 3);
            Assert.NotNull(result.next_id);
        }

        [Fact]
        public void PayPal_ShowPayment_Response_Serialization_Test()
        {
            /*
             * This was pulled directly from PayPal documentation at the following url:
             * https://developer.paypal.com/docs/api/payments/#payment_list
             */
            const string json = @"{
              ""id"": ""PAY-0US81985GW1191216KOY7OXA"",
              ""create_time"": ""2017-06-30T23:48:44Z"",
              ""update_time"": ""2017-06-30T23:49:27Z"",
              ""state"": ""approved"",
              ""intent"": ""order"",
              ""payer"": {
                ""payment_method"": ""paypal""
              },
              ""transactions"": [
                {
                  ""amount"": {
                    ""total"": ""41.15"",
                    ""currency"": ""USD"",
                    ""details"": {
                      ""subtotal"": ""30.00"",
                      ""tax"": ""1.15"",
                      ""shipping"": ""10.00""
                    }
                  },
                  ""description"": ""The payment transaction description."",
                  ""item_list"": {
                    ""items"": [
                      {
                        ""name"": ""hat"",
                        ""sku"": ""1"",
                        ""price"": ""3.00"",
                        ""currency"": ""USD"",
                        ""quantity"": ""5""
                      },
                      {
                        ""name"": ""handbag"",
                        ""sku"": ""product34"",
                        ""price"": ""15.00"",
                        ""currency"": ""USD"",
                        ""quantity"": ""1""
                      }
                    ],
                    ""shipping_address"": {
                      ""recipient_name"": ""John Doe"",
                      ""line1"": ""4th Floor, One Lagoon Drive"",
                      ""line2"": ""Unit #34"",
                      ""city"": ""Redwood City"",
                      ""state"": ""CA"",
                      ""phone"": ""4084217591"",
                      ""postal_code"": ""94065"",
                      ""country_code"": ""US""
                    }
                  },
                  ""related_resources"": [
                    {
                      ""authorization"": {
                        ""id"": ""53P09338XY5426455"",
                        ""create_time"": ""2017-06-30T23:50:01Z"",
                        ""update_time"": ""2017-06-30T23:50:01Z"",
                        ""amount"": {
                          ""total"": ""41.15"",
                          ""currency"": ""USD""
                        },
                        ""parent_payment"": ""PAY-0US81985GW1191216KOY7OXA"",
                        ""valid_until"": ""2017-07-29T23:49:52Z"",
                        ""links"": [
                          {
                            ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-0US81985GW1191216KOY7OXA"",
                            ""rel"": ""parent_payment"",
                            ""method"": ""GET""
                          }
                        ]
                      }
                    }
                  ]
                }
              ],
              ""links"": [
                {
                  ""href"": ""https://api.sandbox.paypal.com/v1/payments/payment/PAY-0US81985GW1191216KOY7OXA"",
                  ""rel"": ""self"",
                  ""method"": ""GET""
                }
              ]
            }";


            var result = _serializer.Deserialize<PayPalPaymentShowResponse>(json);

            Assert.NotNull(result);
            Assert.NotNull(result.payer);
            Assert.NotNull(result.transactions);
            Assert.NotNull(result.links);

            foreach (var trans in result.transactions)
            {
                Assert.NotNull(trans.amount);
                Assert.NotNull(trans.amount.details);
                Assert.NotNull(trans.item_list);
            }
        }

        #endregion
    }
}
