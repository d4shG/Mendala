using Api.Data.Entities;
using Api.Models.CustomerDtos;
using Api.Models.Enums;
using Api.Models.InvoiceDtos;
using Api.Models.InvoiceItemDtos;
using Api.Models.IssueDtos;
using Api.Models.ProductDtos;
using Api.Models.StatusHistoryDtos;

namespace Api.Utils.DtoMapper;

public static class DtoMapper
{
	public static ProductResponseDto ConvertProductToProductResponseDto(Product product) =>
		new ProductResponseDto(product.Id,
			product.Name,
			product.Sku
		);
	
	public static IssueResponseDto ConvertIssueToIssueResponseDto(Issue issue) =>
		new IssueResponseDto(
			issue.Id,
			issue.Title,
			issue.Description,
			issue.CreatorId,
			issue.Creator.UserName ?? "Admin",
			issue.InvoiceId,
			issue.Invoice.InvoiceNumber,
			issue.Invoice.CustomerId,
			issue.Invoice.Customer.Name,
			issue.Status,
			issue.Notes ?? null,
			issue.CreatedAt
		);
	
	public static CustomerResponseDto ConvertCustomerToCustomerResponseDto(Customer customer) =>
		new CustomerResponseDto(customer.Id, 
			customer.Name, 
			customer.Email, 
			customer.Phone, 
			customer.Address);

	private static InvoiceItemResponseDto ConvertInvoiceItemToInvoiceItemResponseDto(InvoiceItem item)
	{
		var product = ConvertProductToProductResponseDto(item.Product);
		
		return new InvoiceItemResponseDto(item.Id,
			product,
			item.PriceAtPurchase,
			item.Quantity
		);
	}
	
	public static InvoiceResponseDto ConvertInvoiceToInvoiceResponseDto(Invoice invoice)
	{
		var items = invoice.InvoiceItems.Select(ConvertInvoiceItemToInvoiceItemResponseDto);
		
		return new InvoiceResponseDto(invoice.Id,
			invoice.InvoiceNumber,
			invoice.Name,
			invoice.Address,
			invoice.CustomerId,
			items
			);
	}

	public static StatusHistoryResponseDto ConvertIssueStatusHistoryToStatusHistoryResponseDto(
		IssueStatusHistory statusHistory) =>
		new StatusHistoryResponseDto(
			statusHistory.Status,
			statusHistory.ChangedAt);

}