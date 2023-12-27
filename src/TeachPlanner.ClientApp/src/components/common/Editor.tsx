import "../../styles/editor.css";
import { LexicalComposer } from "@lexical/react/LexicalComposer";
import { RichTextPlugin } from "@lexical/react/LexicalRichTextPlugin";
import { ContentEditable } from "@lexical/react/LexicalContentEditable";
import { HistoryPlugin } from "@lexical/react/LexicalHistoryPlugin";
import { AutoFocusPlugin } from "@lexical/react/LexicalAutoFocusPlugin";
import AutoLinkPlugin from "../richTextEditor/AutoLinkPlugin";
import ToolbarPlugin from "../richTextEditor/ToolbarPlugin";
import LexicalErrorBoundary from "@lexical/react/LexicalErrorBoundary";
import { HeadingNode, QuoteNode } from "@lexical/rich-text";
import { TableCellNode, TableNode, TableRowNode } from "@lexical/table";
import { ListItemNode, ListNode } from "@lexical/list";
import { AutoLinkNode, LinkNode } from "@lexical/link";
import { LinkPlugin } from "@lexical/react/LexicalLinkPlugin";
import { ListPlugin } from "@lexical/react/LexicalListPlugin";

const editorConfig = { 
	namespace: "MyEditor",
	onError(error: Error) {
		console.log(error)
	},
	nodes: [
		HeadingNode,
		ListNode,
		ListItemNode,
		QuoteNode,
		TableNode,
		TableCellNode,
		TableRowNode,
		AutoLinkNode,
		LinkNode
	]
}

function Placeholder() {
	return <div className="editor-placeholder">Enter lesson notes here</div>
}

export default function RichTextEditor() {
	return (
		<LexicalComposer initialConfig={editorConfig}>
			<div className="editor-container">
				<ToolbarPlugin />
			</div>
			<div className="inner-editor">
			<RichTextPlugin
				contentEditable={<ContentEditable className="editor-input" />}
				placeholder={<Placeholder />}
				ErrorBoundary={LexicalErrorBoundary}
				/>
			<HistoryPlugin />
				<AutoFocusPlugin />
				<ListPlugin />
				<LinkPlugin />
				<AutoLinkPlugin />
			</div>
		</LexicalComposer>
	)
}
