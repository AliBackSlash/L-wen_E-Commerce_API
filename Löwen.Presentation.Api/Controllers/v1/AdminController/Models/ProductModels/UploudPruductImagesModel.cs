namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models.ProductModels;

public record UploudPruductImagesModel(IEnumerable<UploudPruductImages> Uplouds);
public record UploudPruductImages(bool IsMain, IFormFile image);