#pragma once
#include "MyStack.h"

namespace Forma {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// —водка дл€ MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// ќсвободить все используемые ресурсы.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Button^ btnImport;
	private: System::Windows::Forms::TextBox^ txt1;
	private: System::Windows::Forms::TextBox^ txt2;
	protected:



	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Button^ btnSort;

	private: System::Windows::Forms::Button^ btnExport;

	private: System::Windows::Forms::SaveFileDialog^ saveFileDialog1;
	private: System::Windows::Forms::OpenFileDialog^ openFileDialog1;
	private: System::Windows::Forms::Label^ label3;

	private:
		/// <summary>
		/// ќб€зательна€ переменна€ конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// “ребуемый метод дл€ поддержки конструктора Ч не измен€йте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->btnImport = (gcnew System::Windows::Forms::Button());
			this->txt1 = (gcnew System::Windows::Forms::TextBox());
			this->txt2 = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->btnSort = (gcnew System::Windows::Forms::Button());
			this->btnExport = (gcnew System::Windows::Forms::Button());
			this->saveFileDialog1 = (gcnew System::Windows::Forms::SaveFileDialog());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 10));
			this->label1->Location = System::Drawing::Point(12, 36);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(89, 17);
			this->label1->TabIndex = 0;
			this->label1->Text = L"Source data:";
			// 
			// btnImport
			// 
			this->btnImport->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12));
			this->btnImport->Location = System::Drawing::Point(241, 13);
			this->btnImport->Name = L"btnImport";
			this->btnImport->Size = System::Drawing::Size(147, 40);
			this->btnImport->TabIndex = 1;
			this->btnImport->Text = L"Import from file";
			this->btnImport->UseVisualStyleBackColor = true;
			// 
			// txt1
			// 
			this->txt1->Location = System::Drawing::Point(12, 59);
			this->txt1->Multiline = true;
			this->txt1->Name = L"txt1";
			this->txt1->Size = System::Drawing::Size(375, 128);
			this->txt1->TabIndex = 2;
			this->txt1->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txt1_KeyPress);
			// 
			// txt2
			// 
			this->txt2->Location = System::Drawing::Point(12, 248);
			this->txt2->Multiline = true;
			this->txt2->Name = L"txt2";
			this->txt2->ReadOnly = true;
			this->txt2->Size = System::Drawing::Size(375, 126);
			this->txt2->TabIndex = 3;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 10));
			this->label2->Location = System::Drawing::Point(12, 225);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(52, 17);
			this->label2->TabIndex = 4;
			this->label2->Text = L"Result:";
			// 
			// btnSort
			// 
			this->btnSort->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12));
			this->btnSort->Location = System::Drawing::Point(77, 206);
			this->btnSort->Name = L"btnSort";
			this->btnSort->Size = System::Drawing::Size(144, 36);
			this->btnSort->TabIndex = 5;
			this->btnSort->Text = L"Sort wagons";
			this->btnSort->UseVisualStyleBackColor = true;
			this->btnSort->Click += gcnew System::EventHandler(this, &MyForm::btnSort_Click);
			// 
			// btnExport
			// 
			this->btnExport->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12));
			this->btnExport->Location = System::Drawing::Point(241, 206);
			this->btnExport->Name = L"btnExport";
			this->btnExport->Size = System::Drawing::Size(146, 36);
			this->btnExport->TabIndex = 6;
			this->btnExport->Text = L"Export to file";
			this->btnExport->UseVisualStyleBackColor = true;
			// 
			// saveFileDialog1
			// 
			this->saveFileDialog1->DefaultExt = L"txt";
			this->saveFileDialog1->Filter = L"Text files|*.txt";
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			this->openFileDialog1->Filter = L"Text files|*.txt";
			this->openFileDialog1->RestoreDirectory = true;
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(5, 13);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(230, 13);
			this->label3->TabIndex = 7;
			this->label3->Text = L"Railcars number: XY (X - type: A or B, Y - index)";
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(400, 386);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->btnExport);
			this->Controls->Add(this->btnSort);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->txt2);
			this->Controls->Add(this->txt1);
			this->Controls->Add(this->btnImport);
			this->Controls->Add(this->label1);
			this->Name = L"MyForm";
			this->Text = L"SiAOD4 - Railway yard";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
private: System::Void txt1_KeyPress(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e) {
	if (e->KeyChar == 65 || e->KeyChar == 66 || e->KeyChar == 32 ||
		Char::IsDigit(e->KeyChar) || e->KeyChar == 8)
		e->Handled = false;
	else e->Handled = true;
}
	private: System::Void btnSort_Click(System::Object^ sender, System::EventArgs^ e) {
		if (txt1->Text->Length > 4) {
			txt2->ResetText();
			array<String^>^ arr = txt1->Text->Split(' ');
			MyStack::Stack^ stack = gcnew MyStack::Stack();
			int k[] = {0,0};
			for (int i = 0; i < arr->Length; i++)
			{
				stack->push(arr[i]);
				k[0] = whichType(arr[i]);

				if (Math::Abs(k[0] - k[1]) > 0) {
					txt2->AppendText(stack->pop() + " ");
					if (stack->isEmpty()) {
						k[1] = k[0];
					}
					else {
						txt2->AppendText(stack->pop() + " ");
					}
				}
			}
		}
	}
		   
private: int whichType(String^ str) {
	switch (str[0]) {
	case 'A': {
		return 1;	
	}
	case 'B': {
		return 2;	
	}
	default: 
		return 0;	
	}	
}
};
}
