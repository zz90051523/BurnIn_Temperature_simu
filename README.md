# BurnIn Temperature Simulator

## 概述
**BurnIn Temperature Simulator** 是一個 Windows Forms 應用程式，用於模擬和監控多通道的溫度數據。它提供即時的溫度視覺化圖形介面，並允許使用者配置 Modbus RTU 通訊的相關設定。

## 功能
- **溫度模擬**: 模擬8 個通道的溫度數據，並可調整基準溫度與範圍。
- **即時圖表**: 動態顯示溫度數據與平均溫度。
- **Modbus RTU 通訊**: 作為 Modbus Slave，允許外部 Modbus Master讀取與寫入數據。
- **日誌記錄**: 記錄溫度更新與 Modbus 通訊事件。
- **可配置通訊設定**: 支援配置 COM 埠、波特率、數據位元、校驗位元、停止位元與超時時間。
- **互動式圖表**: 使用者可切換圖表中各通道的顯示狀態。

## 系統需求
- **.NET Framework4.7.2**
- 支援 .NET Framework 應用程式的 Windows 環境。
- 搭配BurnIn_System使用。

## 執行方式
1. 克隆此儲存庫：
 ```bash
 git clone https://github.com/zz90051523/BurnIn_Temperature_simu.git
 ```
2. 在 Visual Studio 中開啟解決方案。
3. 建置解決方案以還原相依性並編譯專案。
4. 執行應用程式。

## 使用說明
1. 選擇所需的 COM 埠，並配置通訊設定（波特率、數據位元、校驗位元、停止位元與超時時間）。
2. 點擊 **開啟** 按鈕以開始模擬並啟用 Modbus 通訊。
3. 在圖表與日誌視窗中監控溫度數據。
4. 使用圖表圖例切換各通道的顯示狀態。
5. 點擊 **關閉** 按鈕以停止模擬並關閉 Modbus 通訊。

## 專案結構
- **`Form1.cs`**: 包含應用程式的主要邏輯，包括溫度模擬、Modbus 通訊與圖表更新。
- **`Form1.Designer.cs`**: Windows Forms 設計工具自動生成的檔案。
- **`Program.cs`**: 應用程式的進入點。
- **`Properties`**: 包含組件資訊、資源與設定。

## 核心元件
### Modbus 通訊
- 使用 `NModbus` 函式庫實現 Modbus RTU 通訊。
- 作為 Modbus Slave，提供保持暫存器供外部 Modbus Master互動。

### 溫度模擬
- 根據基準溫度與可調範圍模擬8 個通道的溫度數據。
- 計算平均溫度並即時更新圖表。

### 圖表
- 使用 `System.Windows.Forms.DataVisualization.Charting` 顯示溫度數據。
- 提供互動功能，例如工具提示與通道顯示切換。

## 相依性
- **NModbus**: 用於 Modbus RTU 通訊。
- **System.Windows.Forms.DataVisualization**: 用於圖表顯示。

## 授權
此專案採用 MIT 授權條款。詳情請參閱 LICENSE 檔案。

## 作者
- GitHub: [zz90051523](https://github.com/zz90051523)