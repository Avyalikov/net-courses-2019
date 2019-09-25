// <copyright file="IDTO.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;

    /// <summary>
    /// IDTO description
    /// </summary>
    public interface IDTOMethodsforPriceHistory
    {
         IEnumerable<PriceHistory> FindEntitiesByRequest(int stockId);
        IEnumerable<PriceHistory> FindEntitiesByRequestDTO(PriceArguments DTOarguments);

    }
}
