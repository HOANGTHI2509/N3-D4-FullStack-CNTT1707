# ==============================================================================
# Gõ 'make help' để xem danh sách các lệnh hỗ trợ.
# LƯU Ý: Phía trước các lệnh (docker-compose...) BẮT BUỘC phải là 1 dấu TAB, không được dùng Space.
# ==============================================================================

.PHONY: help up down restart logs clean api-logs db-logs

help: ## Hiển thị danh sách các lệnh
	@echo "Danh sách các lệnh tự động hóa Docker:"
	@echo "  make up        - Build và chạy toàn bộ hệ thống ngầm (API, DB, RabbitMQ)"
	@echo "  make down      - Tắt và xóa các container"
	@echo "  make restart   - Khởi động lại toàn bộ hệ thống"
	@echo "  make logs      - Xem log của tất cả các service (thời gian thực)"
	@echo "  make api-logs  - Chỉ xem log của Backend API"
	@echo "  make clean     - Tắt container và XÓA TOÀN BỘ DATA (Database, RabbitMQ volumes)"

up: ## Build và chạy toàn bộ hệ thống
	docker-compose up -d --build

down: ## Tắt hệ thống
	docker-compose down

restart: down up ## Khởi động lại hệ thống

logs: ## Xem log toàn hệ thống
	docker-compose logs -f

api-logs: ## Xem log của riêng API
	docker-compose logs -f api

db-logs: ## Xem log của service chạy file init.sql
	docker-compose logs -f db-init

clean: ## Dọn dẹp sạch sẽ (Xóa cả dữ liệu trong volume)
	docker-compose down -v
	@echo "Đã dọn dẹp sạch sẽ Container và Volumes!"