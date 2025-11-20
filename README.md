أكيد، هنا README.md احترافي جدًا مكتوب بمهارة وبأسلوب GitHub-ready مع استخدام Markup منسّق، وعناوين واضحة، وإيموجيز مناسبة، وشكل يليق بمشروع كبير.
تقدر تنسخه وتلصقه مباشرة في ملف README.md:


---

# Löwen E-Commerce Platform API

Enterprise-grade clothing commerce backend built on ASP.NET 9, CQRS, and Clean Architecture with an Angular 20 storefront companion.

## Table of Contents
1. [Highlights](#highlights)
2. [Architecture Overview](#architecture-overview)
3. [Functional Scope](#functional-scope)
4. [Project Layout](#project-layout)
5. [Cross-Cutting Platform Services](#cross-cutting-platform-services)
6. [Getting Started](#getting-started)
7. [Configuration Reference](#configuration-reference)
8. [API Exploration & Developer Workflow](#api-exploration--developer-workflow)
9. [Roadmap](#roadmap)
10. [Controller Reference (v1)](#controller-reference-v1)

---

## Highlights
- Clean Architecture solution (Domain → Application → Infrastructure → Presentation) with strict separation of concerns.
- CQRS + MediatR orchestration, FluentValidation pipelines, and cache-aware behaviors for predictable read/write flows.
- ASP.NET Identity with JWT + refresh tokens, configurable rate limiting, and role-based authorization across Admin/User/Delivery/RootAdmin profiles.
- EF Core (Code First) targeting PostgreSQL with 70+ migrations, covering catalog, orders, payments, discounts, and user activity.
- Memory caching, centralized exception middleware, response compression, and versioned Swagger (OpenAPI) for production-ready APIs.
- Angular 20 SPA (see sibling folder `../L-wen_E-Commerce_Frontend`) communicating over HTTPS with CORS policy `AllowAngularDev`.
- Developer ergonomics: `.http` scratch files, reusable email templates, file-upload services, and documented paging/result patterns.

---

## Architecture Overview

```
Angular 20 UI (Authentication, Catalog, Checkout)
            │
            ▼
┌──────────────────────┐      CQRS (MediatR Pipelines)      ┌──────────────────────┐
│ Löwen.Presentation   │  ───────────────────────────────► │ Löwen.Application     │
│ ASP.NET 9 Web API    │  ◄─────────────────────────────── │ Commands & Queries    │
└──────────┬───────────┘                                   └──────────┬───────────┘
           │                                                        │
           ▼                                                        ▼
┌──────────────────────┐                                   ┌──────────────────────┐
│ Global Middleware    │                                   │ Löwen.Domain         │
│ Auth, Rate Limiting, │                                   │ Entities, Enums,     │
│ Exception Handling   │                                   │ Value Objects, DTOs  │
└──────────┬───────────┘                                   └──────────┬───────────┘
           ▼                                                        ▼
                              Löwen.Infrastructure
                      EF Core, Identity, Repositories,
                      Email/File Services, Cache Providers
                               │
                               ▼
                           PostgreSQL
```

---

## Functional Scope
- **Catalog Management**: Products, variants, categories, tags, media, loves, and reviews handled through Admin/User controllers with rich filtering.
- **Promotions & Pricing**: Coupons, discounts, and automatic order recalculations; configurable discount and coupon lifecycles.
- **Cart & Checkout**: Authenticated carts, order drafting, delivery assignment, wishlists, and loves to support personalized storefront flows.
- **Fulfillment**: Delivery controller for rider workloads, order status transitions, and admin assignment batching.
- **Payments**: Initial payment registration and status updates with enum-backed workflow (pending → captured → failed/refunded).
- **Identity & Accounts**: Email confirmation flows, password reset, refresh tokens, RootAdmin for privilege escalation, and per-user profile management.
- **Communications**: Razor-free HTML email templates for password resets, confirmations, OTP, and order status change notifications.
- **Utilities**: Upload service for profile/product imagery with extension/size validation, global result wrapper, pagination helpers, and rate-limited sensitive endpoints.

---

## Project Layout

```
L-wen_E-Commerce_API/
├─ Löwen.Api.sln
├─ Löwen.Domain/             # Entities, value objects, enums, result & pagination helpers
├─ Löwen.Application/        # CQRS features, DTOs, pipeline behaviors, cache abstractions
├─ Löwen.Infrastructure/     # EF Core DbContext, repositories, migrations, services
└─ Löwen.Presentation.Api/   # ASP.NET 9 host, controllers, middleware, configs, .http files
```

Supporting assets:
- `APIEndpointsTest/*.http` – executable REST collections for each controller.
- `EmailTemplates/*.html` – templated transactional emails rendered by the email service.
- `UploadFilesServices/` – strongly validated file uploads for user avatars and product media.
- `Notes/*.txt` – design considerations & background service ideas.

---

## Cross-Cutting Platform Services
- **Authentication & Authorization**: ASP.NET Identity + JWT bearer auth with refresh tokens, configurable issuer/audience, and role isolation for `Admin`, `RootAdmin`, `User`, and `Delivery`.
- **Rate Limiting**: Fixed-window policies (`LoginPolicy`, `ResetPasswordPolicy`, `VerifyEmailPolicy`, `RefreshTokenPolicy`) throttling sensitive operations through `AddRateLimiterPolicies`.
- **Caching**: `QueryCachingBehavior` & `InvalidateCacheBehavior` coordinate with `ICacheService` (memory-based tokenized prefix invalidation) for read-heavy endpoints.
- **Validation Pipeline**: FluentValidation automatically enforces request contracts via `ValidationPipelineBehavior<TRequest, TResponse>`.
- **Error Handling**: `ExceptionHandlingMiddleware` standardizes failures into the Domain `Result/Error` envelope; paired with domain-level `ErrorCodes` for traceability.
- **Observability & Performance**: Response compression, paging guards (`PaginationSettings.maxPageSize`), strongly typed configuration objects, and consistent result wrappers.
- **Static Assets**: `StaticFilesSettings` centralize upload destinations and image rules; `FileService` enforces quotas/extensions.

---

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [Node.js 20 LTS](https://nodejs.org/) + `npm install -g @angular/cli@20` (if running the Angular front-end)
- `dotnet-ef` CLI tool: `dotnet tool install --global dotnet-ef`

### 1. Clone & Restore
```bash
git clone https://github.com/<org>/L-wen_E-Commerce_API.git
cd L-wen_E-Commerce_API
dotnet restore Löwen.Api.sln
```

### 2. Configure Secrets
Update `Löwen.Presentation.Api/appsettings.Development.json` (or use `dotnet user-secrets`) with:
- `ConnectionStrings:DefaultConnection`
- `JWT` issuer/audience/signing key
- `StaticFilesSettings` paths and quotas
- `ApiSettings` host URLs

### 3. Apply Database Migrations
```bash
dotnet ef database update \
  --project Löwen.Infrastructure \
  --startup-project Löwen.Presentation.Api \
  --context AppDbContext
```

### 4. Run the API
```bash
dotnet run --project Löwen.Presentation.Api
```
Browse to `https://localhost:7197/swagger` for versioned OpenAPI docs. Development mode automatically applies migrations and hosts Swagger at the root route.

### 5. (Optional) Run the Angular Client
```bash
cd ../L-wen_E-Commerce_Frontend
npm install
ng serve --open
```

---

## Configuration Reference

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=löwendb;Username=postgres;Password=***"
  },
  "JWT": {
    "Issuer": "LocalHost",
    "Audience": "LocalHost",
    "Duration": 30,
    "RefreshTokenDuration": 7,
    "SigningKey": "<64-char dev key>"
  },
  "StaticFilesSettings": {
    "ProfileImages_FileName": "Uploades/Profiles_Images/",
    "ProductImages_FileName": "Uploades/Products_Image/",
    "MaxProfileImageSize": 2048,
    "MaxProductImageSize": 2048,
    "AllowedImageExtensions": [ ".jpg", ".png", ".jpeg", ".gif", ".bmp", ".webp" ]
  },
  "PaginationSettings": {
    "MaxPageSize": 50
  },
  "ApiSettings": {
    "BaseUrl": "https://localhost:7197",
    "OrderPageUrl": "https://localhost:7197/Orders"
  }
}
```

> Production deployments should move secrets to secure stores (KeyVault, AWS Secrets Manager, etc.) and replace the sample signing key.

---

## API Exploration & Developer Workflow
- **Swagger / OpenAPI**: Enabled in Development with XML comments; supports multiple versions via `AddVersionedApiExplorer`.
- **`.http` Scratch Files**: `APIEndpointsTest/*.http` mirror each controller and include example payloads plus bearer token placeholders for quick manual tests (Hoppscotch/Postman compatible).
- **Result & Error Pattern**: Every action returns `Result`/`Result<T>` ensuring consistent envelope, status mapping, and error aggregation. Failed validations automatically populate `ErrorCodes`.
- **Pagination**: Route parameters follow the `/{PageNumber},{PageSize}` convention with server-side caps; invalid params trigger `400 BadRequest`.
- **Authentication Flow**: Register/Login → receive JWT/refresh token → call protected routes with `Authorization: Bearer {token}`. Refresh endpoints are rate limited and leverage persisted `UserRefreshToken`.
- **File Uploads**: Multipart routes (profile/product images) enforce file settings via `FileService`. Responses include `UploadResponse` metadata.
- **Email Templates**: Located under `EmailTemplates/`, consumed by `EmailService` for OTP, password reset, confirmation, and order status notifications.
- **Developer Commands**
  - `dotnet watch --project Löwen.Presentation.Api` for hot reload.
  - `dotnet format Löwen.Api.sln` to enforce code style.
  - `dotnet test` *(tests are planned; add once test projects are introduced)*.

---

## Roadmap
- Expand `PaymentController` with query endpoints and integrations (Stripe, PayPal, local gateways).
- Implement `NotificationController` real-time endpoints (SignalR/WebPush) and background workers outlined in `Notes/BackgroundServicesAtDatabase.txt`.
- Add integration/unit test suites plus automated quality gates (GitHub Actions).
- Introduce cache invalidation policies per aggregate, plus distributed cache providers (Redis) for horizontal scaling.
- Harden upload storage (Azure Blob/S3) and switch from local filesystem to cloud buckets.
- Extend catalog filters (tags, ratings, age/kind) and advanced search endpoints currently stubbed in `ProductsController`.

---

## Controller Reference (v1)

Documentation for every controller under `Controllers/v1`. Use this as a quick reference when exercising HTTP endpoints from Swagger, Hoppscotch, Postman, or automated tests.

### Quick Usage Notes
- **Base URL**: Routes are relative to the ASP.NET host root (e.g., `https://localhost:7197/`).
- **Versioning**: Controllers expose `ApiVersion("1.0")`; routes already include `api/{Resource}` so `/v1` is implicit.
- **Authentication**: Unless otherwise noted, endpoints require a valid JWT bearer token and appropriate role (`[Authorize(Roles = "...")]`).
- **Result Wrapper**: Successful actions return `Result/Result<T>` envelopes mapped to `200/201/204`. Failures return one or more `Error` items.
- **Paging Pattern**: Paged routes expect `/api/<resource>/<action>/{PageNumber},{PageSize}` (`/api/Admin/get-products-paged/1,10`).
- **Rate Limiting**: Policies such as `LoginPolicy`, `ResetPasswordPolicy`, `VerifyEmailPolicy`, and `RefreshTokenPolicy` are enforced where attributes are present.

### Controller Index

| Controller | Base Route | Roles | Purpose |
| --- | --- | --- | --- |
| `AdminController` | `/api/Admin` | `Admin` | Back-office management for categories, products, users, delivery assignments. |
| `AuthController` | `/api/Auth` | Anonymous | Registration, login, email confirmation, credential recovery, token refresh. |
| `CartController` | `/api/Cart` | `User` | Manage the authenticated shopper's cart. |
| `CouponController` | `/api/Coupon` | `Admin` (order endpoints override to `User`) | Full CRUD plus apply/remove coupon on orders. |
| `DeliveryController` | `/api/Delivery` | `Delivery` | Delivery agent work queue and status updates. |
| `DiscountController` | `/api/Discount` | `Admin` | CRUD and listing for catalog discounts. |
| `EmailController` | `/api/Email` | `User,Admin` | Trigger confirmation emails. |
| `NotificationController` | `/api/Notification` | — | Placeholder (not implemented in v1). |
| `OrderController` | `/api/Order` | `User` (admin-only listing) | Place orders, edit items, fetch order details/history. |
| `PaymentController` | `/api/Payment` | `User,Admin` | Add payments and update payment status (read endpoints not implemented). |
| `ProductsController` | `/api/Product` | `User` | Catalog browsing, product detail & review reads. |
| `RootAdminController` | `/api/RootAdmin` | `RootAdmin` | Super-admin functions: create admins, manage roles, soft delete/restore. |
| `UsersController` | `/api/users` | `User` | Self-service profile, wishlist, loves, reviews, orders. |

---

### AdminController (`/api/Admin`, role: `Admin`)

| Method | Route | Body/Params | Description |
| --- | --- | --- | --- |
| `POST` | `/add-category` | `AddCategoryModel` | Create a category with name + gender. |
| `PUT` | `/update-category` | `UpdateCategoryModel` | Rename/change gender on an existing category. |
| `DELETE` | `/remove-category/{Id:guid}` | `Id` route | Remove a category by GUID. |
| `GET` | `/get-products-paged/{PageNumber},{PageSize}` | route paging | List products (no filter). |
| `GET` | `/get-products-paged-filter-by-id-or-name/{IdOrName},{PageNumber},{PageSize}` | route filter | Search products by ID/name. |
| `POST` | `/add-product` | `AddProductModel` | Create catalog product (metadata + variants + tags). |
| `POST` | `/upload-product-images/{ProductId}` | multipart `UploudPruductImagesModel` | Upload image files + associate with product. |
| `DELETE` | `/remove-product-image/{imagePath}` | route path | Delete a product image (also removes file). |
| `PUT` | `/update-product` | `UpdateProductModel` | Update product metadata (name, description, price, status, category). |
| `POST` | `/add-product-variant` | `AddProductVariantModel` | Add a variant (color/size/price/stock). |
| `PUT` | `/update-product-variant` | `UpdateProductModel` | Edit variant attributes. |
| `DELETE` | `/remove-product-variant/{Id}` | route id | Delete variant. |
| `DELETE` | `/remove-product/{Id}` | route id | Delete product and related data. |
| `PUT` | `/assigned-orders-to-delivery` | `AssignedOrdersToDeliveryModel` | Batch-assign orders to delivery entities. |
| `GET` | `/get-user-by-id/{Id}` | route id | Fetch user profile by ID. |
| `GET` | `/get-user-by-email/{email}` | route email | Fetch user by email. |
| `GET` | `/get-users-paged/{PageNumber},{PageSize}` | paging | List users. |
| `PUT` | `/mark-user-as-deleted/{Id}` | route guid | Soft-delete user. |
| `PUT` | `/activate-marked-user-as-deleted/{Id}` | route guid | Restore soft-deleted user. |

### AuthController (`/api/Auth`, anonymous)

| Method | Route | Body/Params | Notes |
| --- | --- | --- | --- |
| `POST` | `/register` | `RegisterModel` | Create user; returns 201 + user info. |
| `POST` | `/login` | `LoginModel` | Rate limited (`LoginPolicy`); returns tokens. |
| `POST` | `/confirm-email` | query `userId`, `confirmEmailToken` | Confirms email; expects query parameters. |
| `PUT` | `/reset-password` | `ResetPasswordModel` | Rate limited (`ResetPasswordPolicy`). |
| `PUT` | `/refresh-token/{refreshToken}` | route token | Exchange refresh token for new access token. |

### CartController (`/api/Cart`, role: `User`)

| Method | Route | Body/Params | Description |
| --- | --- | --- | --- |
| `POST` | `/add-to-cart` | `CartItemModel` `{ ProductId, Quantity }` | Adds/increments cart item for current user. |
| `DELETE` | `/remove-from-cart/{CartId},{ProductId}` | route tuple | Removes item from cart. |
| `PUT` | `/update-cart-item-quantity/{CartId},{ProductId},{Quantity}` | route tuple | Adjusts quantity of an item. |
| `GET` | `/get-cart-by-user/{PageNumber},{PageSize}` | paging | Returns paged cart items for authenticated user. |

### CouponController (`/api/Coupon`)

Controller default role: `Admin`. The two order-level routes override to `[Authorize(Roles = "User")]`.

| Method | Route | Body/Params | Description |
| --- | --- | --- | --- |
| `POST` | `/add-coupon` | `AddCouponModel` | Create coupon definition. |
| `PUT` | `/update-coupon` | `UpdateCouponModel` | Update coupon fields. |
| `DELETE` | `/remove-coupon/{Id}` | route id | Delete coupon. |
| `POST` | `/apply-coupon-to-order/{CouponCode},{OrderId}` | route tuple, **User role** | Apply coupon to order. |
| `DELETE` | `/remove-coupon-from-order/{CouponCode},{OrderId}` | route tuple, **User role** | Remove coupon from order. |
| `GET` | `/get-coupon-by-id/{Id}` | route id | Fetch coupon. |
| `GET` | `/get-coupon-by-code/{Code}` | route code | Fetch coupon by public code. |
| `GET` | `/get-all-coupons-paged/{PageNumber},{PageSize}` | paging | List coupons with pagination. |

### DeliveryController (`/api/Delivery`, role: `Delivery`)

| Method | Route | Params | Description |
| --- | --- | --- | --- |
| `GET` | `/get-assigned-orders/{PageNumber},{PageSize}` | paging | Paged orders assigned to current delivery agent. |
| `PUT` | `/update-order-status/{Id},{Status}` | order id + enum | Update order status (validates transitions). |

### DiscountController (`/api/Discount`, role: `Admin`)

| Method | Route | Body/Params | Description |
| --- | --- | --- | --- |
| `POST` | `/add-discount` | `AddDiscountModel` | Create discount definition. |
| `PUT` | `/update-discount` | `UpdateDiscountModel` | Update discount fields. |
| `DELETE` | `/remove-discount/{Id}` | route id | Delete discount. |
| `GET` | `/get-discount-by-id/{Id}` | route id | Fetch discount detail. |
| `GET` | `/get-all-discounts-paged/{PageNumber},{PageSize}` | paging | Paged discount listing. |

### EmailController (`/api/Email`, roles: `User,Admin`)

| Method | Route | Params | Description |
| --- | --- | --- | --- |
| `POST` | `/send-confirmation-email-token` | query `email` | Rate limited (`VerifyEmailPolicy`); queues confirmation email token. |

### NotificationController

`NotificationController.cs` only contains commented stubs. No v1 notification endpoints are available yet.

### OrderController (`/api/Order`, role: `User`)

| Method | Route | Body/Params | Description |
| --- | --- | --- | --- |
| `POST` | `/add-order` | `OrderItemModel` | Create order for current user. |
| `PUT` | `/update-order-items` | `UpdateOrderItemModel` | Adjust quantity/price/delivery for order items. |
| `GET` | `/get-order-details/{Id}` | route id | Detailed view of a single order. |
| `GET` | `/get-orders-by-user-paged/{PageNumber},{PageSize}` | paging | Current user's order history. |
| `GET` | `/get-orders-paged/{PageNumber},{PageSize}` | paging, **Admin role** | Global order list (admin only). |

### PaymentController (`/api/Payment`, roles: `User,Admin`)

| Method | Route | Status | Notes |
| --- | --- | --- | --- |
| `GET` | `/get-payment-by-id` | ⚠️ Not implemented | Method throws `NotImplementedException`. |
| `GET` | `/get-payments-by-order` | ⚠️ Not implemented | Planned search endpoint. |
| `POST` | `/add-payment` | Implemented | Create payment from `AddPaymentModel`. |
| `PUT` | `/update-payment-status/{Id},{status}` | Implemented | Update payment status (enum `PaymentStatus`). |

### ProductController (`/api/Product`, role: `User`)

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/get-all-products-paged/{PageNumber},{PageSize}` | Base product listing. |
| `GET` | `/get-products-by-name-paged/{Name},{PageNumber},{PageSize}` | Search by name. |
| `GET` | `/get-product-by-id/{productId}` | Single product detail. |
| `GET` | `/get-product-review-by-product-id/{productId},{PageNumber},{PageSize}` | Paged review list. |
| `GET` | `/get-products-by-category/{category},{PageNumber},{PageSize}` | Filter by category slug/id. |
| `GET` | `/get-most-loved-products/{PageNumber},{PageSize}` | Paged list of most-loved items. |
| `GET` | `/get-products-by-gender-paged/{Gender},{PageNumber},{PageSize}` | Filter by gender flag. |

> Source file also contains commented stubs for future filters (tags, ratings, age, kind, combined filters).

### RootAdminController (`/api/RootAdmin`, role: `RootAdmin`)

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/add-admin` | Create admin (credentials + profile). |
| `POST` | `/assign-role/{Id:guid},{role}` | Add role to user. |
| `DELETE` | `/Remove-Role-From-User-admin/{Id:guid},{role}` | Remove role assignment. |
| `PUT` | `/mark-admin-as-deleted/{Id:guid}` | Soft-delete admin. |
| `PUT` | `/activate-marked-as-deleted/{Id:guid}` | Restore admin. |
| `DELETE` | `/remove-admin/{Id:guid}` | Hard-delete admin. |
| `GET` | `/admin-by-id/{Id:guid},{role}` | Query admin by ID + role. |
| `GET` | `/admin-by-email/{Email},{role}` | Query admin by email + role. |
| `GET` | `/admins/{role}` | List admins filtered by role (returns `List<GetAdminsQueryResponse>`). |

### UsersController (`/api/users`, role: `User`)

Self-service endpoints for authenticated customers.

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/get-user-info` | Current user's profile. |
| `PUT` | `/update-user-info` | Update profile (names, DOB, gender, address, phone). |
| `PUT` | `/change-password` | Change password (requires current + new). |
| `PUT` | `/update-profile-image` | Upload + set profile image (`IFormFile`). |
| `PUT` | `/remove-user-image` | Remove profile image (deletes file). |
| `POST` | `/verify-own-email/{email}` | Request confirmation token for own email. |
| `POST` | `/add-product-to-wishlist/{productId}` | Wishlist add. |
| `DELETE` | `/remove-product-from-wishlist/{productId}` | Wishlist remove. |
| `POST` | `/add-love-for-product/{productId}` | Mark product as loved. |
| `DELETE` | `/remove-love-from-product/{productId}` | Remove love. |
| `POST` | `/add-review-for-product` | Create product review (`AddProductReviewModel`). |
| `PUT` | `/update-review-for-product` | Update review (`UpdateProductReviewModel`). |
| `DELETE` | `/remove-review-from-product/{Id}` | Delete review. |
| `GET` | `/get-orders-by-user/{PageNumber},{PageSize}` | Paged order history (user-level). |
| `GET` | `/get-my-wishlist/{PageNumber},{PageSize}` | Paged wishlist listing. |

> The controller also contains commented plans for phone verification and listing own reviews. Those routes are inactive in v1.

---

### Testing the Endpoints Quickly
1. **Swagger/OpenAPI**: Run the API (`dotnet run` or Visual Studio) and open `/swagger`. Each controller above appears under its tag; XML summaries mirror this document.
2. **HTTP scratch files**: Use `.http` scripts under `APIEndpointsTest/` (e.g., `APIEndpointsTest/Admin.http`) to exercise endpoints with sample payloads and bearer tokens.
3. **Authentication**: Obtain JWTs via `POST /api/Auth/login` or the `register → EmailController → confirm-email` flow. Include `Authorization: Bearer {token}` for secured routes.
4. **File uploads**: Endpoints like `/api/Admin/upload-product-images/{ProductId}` and `/api/users/update-profile-image` expect `multipart/form-data`. Use REST clients that support file selection.
5. **Paging**: Provide both `PageNumber` and `PageSize` route segments separated by a comma (e.g., `/api/Product/get-all-products-paged/1,20`). Invalid values return `400 Bad Request`.

_This README should stay in sync with any new controllers, behaviors, or infrastructure changes under `Controllers/v1`._
