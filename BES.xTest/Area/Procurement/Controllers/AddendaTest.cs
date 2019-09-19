using BES.Areas.Procurement.Controllers;
using BES.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace BES.xTest.Area.Procurement.Controllers
{    
    public class AddendaTest
    {
        private readonly ApplicationDbContext _context;
        [Fact]
        public void Index()
        {
            // Arrange
            var addendum = new AddendaController(_context);

            // Act
            var result = addendum.Index();

            //Assert
            Assert.NotNull(result);
        }
    }
}
