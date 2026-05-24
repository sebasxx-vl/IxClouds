export interface SaleItem { productId: number; quantity: number; }
export interface CreateSaleRequest { items: SaleItem[]; }
export interface SaleDetail { productId: number; productName: string; quantity: number; unitPrice: number; subtotal: number; }
export interface Sale { id: number; date: Date; total: number; items: SaleDetail[]; }
