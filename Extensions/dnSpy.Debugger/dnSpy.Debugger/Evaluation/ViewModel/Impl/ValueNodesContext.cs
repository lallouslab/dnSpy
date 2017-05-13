﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using dnSpy.Contracts.Controls.ToolWindows;
using dnSpy.Contracts.Text.Classification;
using dnSpy.Debugger.UI;
using Microsoft.VisualStudio.Text.Classification;

namespace dnSpy.Debugger.Evaluation.ViewModel.Impl {
	interface IValueNodesContext {
		UIDispatcher UIDispatcher { get; }
		DbgValueNodeImageReferenceService ValueNodeImageReferenceService { get; }
		DbgValueNodeReader ValueNodeReader { get; }
		IClassificationFormatMap ClassificationFormatMap { get; }
		ITextElementProvider TextElementProvider { get; }
		TextClassifierTextColorWriter TextClassifierTextColorWriter { get; }
		ValueNodeFormatter Formatter { get; }
		bool SyntaxHighlight { get; }
		DbgValueNodeFormatParameters ValueNodeFormatParameters { get; }
		RefreshNodeOptions RefreshNodeOptions { get; set; }
		string WindowContentType { get; }
		string NameColumnName { get; }
		string ValueColumnName { get; }
		string TypeColumnName { get; }
		ShowYesNoMessageBox ShowYesNoMessageBox { get; }
		IEditValueProvider ValueEditValueProvider { get; }
	}

	sealed class ValueNodesContext : IValueNodesContext {
		public UIDispatcher UIDispatcher { get; }
		public DbgValueNodeImageReferenceService ValueNodeImageReferenceService { get; }
		public DbgValueNodeReader ValueNodeReader { get; }
		public IClassificationFormatMap ClassificationFormatMap { get; }
		public ITextElementProvider TextElementProvider { get; }
		public TextClassifierTextColorWriter TextClassifierTextColorWriter { get; }
		public ValueNodeFormatter Formatter { get; }
		public bool SyntaxHighlight { get; set; }
		public DbgValueNodeFormatParameters ValueNodeFormatParameters { get; }
		public RefreshNodeOptions RefreshNodeOptions { get; set; }
		public string WindowContentType { get; }
		public string NameColumnName { get; }
		public string ValueColumnName { get; }
		public string TypeColumnName { get; }
		public ShowYesNoMessageBox ShowYesNoMessageBox { get; }

		public IEditValueProvider ValueEditValueProvider {
			get {
				UIDispatcher.VerifyAccess();
				if (valueEditValueProvider == null)
					valueEditValueProvider = editValueProviderService.Create(WindowContentType, Array.Empty<string>());
				return valueEditValueProvider;
			}
		}
		IEditValueProvider valueEditValueProvider;
		readonly EditValueProviderService editValueProviderService;

		public ValueNodesContext(UIDispatcher uiDispatcher, string windowContentType, string nameColumnName, string valueColumnName, string typeColumnName, EditValueProviderService editValueProviderService, DbgValueNodeImageReferenceService dbgValueNodeImageReferenceService, DbgValueNodeReader dbgValueNodeReader, IClassificationFormatMap classificationFormatMap, ITextElementProvider textElementProvider, ShowYesNoMessageBox showYesNoMessageBox) {
			UIDispatcher = uiDispatcher;
			WindowContentType = windowContentType;
			NameColumnName = nameColumnName;
			ValueColumnName = valueColumnName;
			TypeColumnName = typeColumnName;
			ShowYesNoMessageBox = showYesNoMessageBox;
			this.editValueProviderService = editValueProviderService;
			ValueNodeImageReferenceService = dbgValueNodeImageReferenceService;
			ValueNodeReader = dbgValueNodeReader;
			ClassificationFormatMap = classificationFormatMap;
			TextElementProvider = textElementProvider;
			TextClassifierTextColorWriter = new TextClassifierTextColorWriter();
			Formatter = new ValueNodeFormatter();
			ValueNodeFormatParameters = new DbgValueNodeFormatParameters();
		}
	}
}
