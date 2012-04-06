using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
namespace Util.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestProduct()
        {
            IProductService productRepository = MockRepository.GenerateMock<IProductService>();
            productRepository.Stub(x => x.Quntity).Return(4);
            Assert.AreEqual(3, productRepository.Quntity);
        }
    }
}
