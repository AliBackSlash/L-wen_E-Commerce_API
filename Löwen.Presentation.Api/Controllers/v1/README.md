# v1 API Controller Reference

Documentation for every controller under `Controllers/v1`. Use this as a quick reference when exercising the HTTP endpoints from Swagger, REST clients (Hoppscotch, VS Code REST, Postman), or automated tests.

## Quick Usage Notes

- **Base URL**: All routes below are relative to the ASP.NET host root (e.g. `https://localhost:5001/`).
- **Versioning**: Controllers are tagged with `ApiVersion("1.0")`; routes already include `api/{Resource}` so you do not add `/v1` explicitly.
- **Authentication**: Unless otherwise noted, endpoints expect a valid JWT Bearer token. Roles come from the `[Authorize]` attribute on the controller/action.
- **Result wrapper**: Actions return the solution-wide `Result`/`Result<T>` shape. On success the payload is mapped into standard HTTP responses (`200`, `201`, `204`). On failure the response body contains one or more `Error` objects.
- **Paging pattern**: Paged routes encode comma-separated route parameters: `/api/Admin/get-products-paged/{PageNumber},{PageSize}` → `/api/Admin/get-products-paged/1,10`.
- **Rate limiting**: Where present (`[EnableRateLimiting]`), respect the named policy configured in `Program.cs`.

## Controller Index

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
| `PUT` | `/update-product-variant` | `UpdateProductVariantModel` | Edit variant attributes. |
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

> The source file also contains commented stubs for future filters (tags, ratings, age, kind, combined filters).

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
| `GET` | `/admins/{role}` | List admins filtered by role (returns `List<GetdminsQueryResponse>`). |

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

## Testing the Endpoints Quickly

1. **Swagger/OpenAPI**: Run the API project (`dotnet run` or from Visual Studio) and open `/swagger`. Each controller above appears under its tag; the summaries from the XML comments match what is described here.
2. **HTTP scratch files**: Use the ready-to-run `.http` scripts under `APIEndpointsTest/` (e.g., `APIEndpointsTest/Admin.http`) to exercise the endpoints with sample payloads and bearer tokens.
3. **Authentication**: Obtain JWTs via `POST /api/Auth/login` or `register` → `EmailController` → `confirm-email` flow. Include the token in `Authorization: Bearer {token}` for any secured route.
4. **File uploads**: Endpoints like `/api/Admin/upload-product-images/{ProductId}` and `/api/users/update-profile-image` expect `multipart/form-data`. Use REST clients that support file selection.
5. **Paging**: Provide both `PageNumber` and `PageSize` route segments separated by a comma (e.g., `/api/Product/get-all-products-paged/1,20`). Supplying invalid values results in `400 Bad Request`.

This README should be kept in sync with any new controllers or endpoints added under `Controllers/v1`.


