export interface Product {
  id: number;
  brand: string;
  phoneModel: string;
  gender: string;
  material: string;
  stock: number;
  purchasePrice: number;
  salePrice: number;
  imageUrl: string;
  createdAt: Date;
  stockStatus?: string;
}
export interface CreateProductRequest {
  brand: string;
  phoneModel: string;
  gender: string;
  material: string;
  stock: number;
  purchasePrice: number;
  salePrice: number;
  imageUrl: string;
}
export interface UpdateProductRequest extends CreateProductRequest { id: number; }
export interface SearchProductRequest { brand?: string; phoneModel?: string; material?: string; gender?: string; }
