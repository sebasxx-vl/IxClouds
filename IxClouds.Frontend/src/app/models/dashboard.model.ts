import { Product } from './product.model';
export interface BestSeller { productId: number; productName: string; totalSold: number; totalRevenue: number; }
export interface DashboardStats { totalSalesAmount: number; todaySalesAmount: number; totalInventoryCount: number; totalSalesCount: number; bestSellingProducts: BestSeller[]; lowStockProducts: Product[]; }
