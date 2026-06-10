export interface Product {
  id: number;
  name: string;
  sku: string;
  description: string;
  category: string;
  stockQuantity: number;
  minStockLevel: number;
  cost: number;
  price: number;
  imageUrl: string;
  isActive: boolean;
  hasLowStock: boolean;
  profitMargin: number;
  createdAt: Date;
  updatedAt: Date;
}

export interface CreateProductRequest {
  name: string;
  sku: string;
  description: string;
  category: string;
  stockQuantity: number;
  minStockLevel: number;
  cost: number;
  price: number;
  imageUrl: string;
  isActive: boolean;
}

export interface UpdateProductRequest extends CreateProductRequest {
  id: number;
}

export interface SearchProductRequest {
  searchTerm?: string;
  category?: string;
  minPrice?: number;
  maxPrice?: number;
  lowStockOnly?: boolean;
  isActive?: boolean;
  pageNumber?: number;
  pageSize?: number;
}