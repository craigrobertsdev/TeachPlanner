// this file, ToolbarPlugin and AutoLinkPlugin are courtesy of https://codesandbox.io/p/sandbox/lexical-rich-text-example-5tncvy?file=%2Fsrc%2Fstyles.css%3A43%2C7
import "../../styles/editor.css";
import { LexicalComposer } from "@lexical/react/LexicalComposer";
import { RichTextPlugin } from "@lexical/react/LexicalRichTextPlugin";
import { ContentEditable } from "@lexical/react/LexicalContentEditable";
import { HistoryPlugin } from "@lexical/react/LexicalHistoryPlugin";
import { AutoFocusPlugin } from "@lexical/react/LexicalAutoFocusPlugin";
import { OnChangePlugin } from "@lexical/react/LexicalOnChangePlugin";
import { TabIndentationPlugin } from "@lexical/react/LexicalTabIndentationPlugin";
import AutoLinkPlugin from "../richTextEditor/AutoLinkPlugin";
import ToolbarPlugin from "../richTextEditor/ToolbarPlugin";
import LexicalErrorBoundary from "@lexical/react/LexicalErrorBoundary";
import { HeadingNode, QuoteNode } from "@lexical/rich-text";
import { TableCellNode, TableNode, TableRowNode } from "@lexical/table";
import { ListItemNode, ListNode } from "@lexical/list";
import { AutoLinkNode, LinkNode } from "@lexical/link";
import { LinkPlugin } from "@lexical/react/LexicalLinkPlugin";
import { ListPlugin } from "@lexical/react/LexicalListPlugin";
import { EditorThemeClasses, EditorState, LexicalEditor } from "lexical";

type EditorProps = {
	onChange: (editorState: EditorState, editor: LexicalEditor, tags: Set<string>) => void;
}

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
	],
	theme: {
		list: {
			ul: "editor-listitem",
			ol: "editor-ol",
		},
		text: {
			underline: "editor-text-underline",
			bold: "editor-text-bold",
			italic: "editor-text-italic",
			strikethrough: "editor-text-strikethrough",
			underlineStrikethrough: "editor-text-underlineStrikethrough",
		}
	} as EditorThemeClasses
}

function Placeholder() {
	return <div className="editor-placeholder">Enter lesson notes here</div>
}

export default function RichTextEditor({ onChange }: EditorProps) {
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
				<TabIndentationPlugin />
				<OnChangePlugin onChange={onChange} />
			</div>
		</LexicalComposer>
	)
}
