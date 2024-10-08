global using Ordering.Orders.Models;
global using Shared.DDD;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using Ordering.Orders.Events;
global using Ordering.Orders.ValueObjects;
global using Microsoft.EntityFrameworkCore;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Ordering.Data;
global using FluentValidation;
global using Ordering.Orders.Dtos;
global using Shared.Contracts.CQRS;
global using Carter;
global using Microsoft.AspNetCore.Routing;
global using Mapster;
global using Microsoft.AspNetCore.Http;
global using Shared.Exceptions;
global using Ordering.Orders.Exceptions;
global using Shared.Pagination;