﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Categories;

public interface ICategoryService
{
    bool CategoryExists(string categoryName);
}
