using json2obj_lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonTreeGrid
{
    public partial class JsonTreeGrid : Form
    {
        //Version 1.0.0
        public JsonTreeGrid()
        {
            InitializeComponent();
        }

        //Da lascaire aperta, le altre si possono collassare
        private void Form1_Load(object sender, EventArgs e)
        {
            //Se l' oggetto è un JSObject, JSArray, JSArrayNK allorabisogna entrarci dentro e itarare
            //l' inzio sarà un JSObjectNK

            //test to replace in recursive_riempimento, under
            //JSObject jSObject  = internalTest();

            TreeNode treeNode = new TreeNode();
            //treeNode.Nodes. ("JsonFile");
            recursive_riempimento(new JSObject(), ref treeNode);
            treeNode.Name = "JSObjectNK";
            treeNode.Text = "JsonFile";
            treeView1.Nodes.Add(treeNode);
            //treeView1.Nodes[0].Text = "JsonFile";
            
            resetTree();
        }

        private JSObject internalTest()
        {
            JSObject jSObject = new JSObject();
            JSString jSString = new JSString() { Key = "key", Value = "valore" };
            JSString jSString2 = new JSString() { Key = "qwe", Value = "41244" };
            JSBool jSBool = new JSBool() { Key = "1442", Value = true };
            jSObject.Value.Add(jSString);
            jSObject.Value.Add(jSString2);
            jSObject.Value.Add(jSBool);
            JSObject jSObject2 = new JSObject();
            jSObject2.Key = "oggetto2";
            jSObject2.Value.Add(jSString);
            jSObject.Value.Add(jSObject2);
            JSArray jSArray = new JSArray();
            jSArray.Key = "array1";
            JSStringNK jSString3 = new JSStringNK() { Value = "41244" };
            jSArray.Value.Add(jSString3);
            jSObject.Value.Add(jSArray);
            return jSObject;
        }

        private void recursive_riempimento(JSObject jSObject, ref TreeNode treeNode)
        {
            int temp_count = 0;
            foreach (JSGeneric jSGeneric in jSObject.Value) 
            {
                if (jSGeneric is JSBool) {
                    JSBool value = (JSBool)jSGeneric;
                    treeNode.Nodes.Add(value.Key);
                    treeNode.Nodes[temp_count].Nodes.Add(value.Value.ToString());
                    treeNode.Nodes[temp_count].Nodes[0].Name = "JSBoolValue";
                    treeNode.Nodes[temp_count].Name = "JSBool";
                    temp_count++;
                }
                if (jSGeneric is JSDouble)
                {
                    JSDouble value = (JSDouble)jSGeneric;
                    treeNode.Nodes.Add(value.Key);
                    treeNode.Nodes[temp_count].Nodes.Add(value.Value.ToString());
                    treeNode.Nodes[temp_count].Nodes[0].Name = "JSDoubleValue";
                    treeNode.Nodes[temp_count].Name = "JSDouble";
                    temp_count++;
                }
                if (jSGeneric is JSNull)
                {
                    JSNull value = (JSNull)jSGeneric;
                    treeNode.Nodes.Add(value.Key);
                    treeNode.Nodes[temp_count].Nodes.Add("null");
                    treeNode.Nodes[temp_count].Nodes[0].Name = "JSNullValue";
                    treeNode.Nodes[temp_count].Name = "JSNull";
                    temp_count++;
                }
                if (jSGeneric is JSString)
                {
                    JSString value = (JSString)jSGeneric;
                    treeNode.Nodes.Add(value.Key);
                    treeNode.Nodes[temp_count].Nodes.Add(value.Value.ToString());
                    treeNode.Nodes[temp_count].Nodes[0].Name = "JSStringValue";
                    treeNode.Nodes[temp_count].Name = "JSString";
                    temp_count++;
                }
                if (jSGeneric is JSObject)
                {
                    JSObject jSObject2 = (JSObject)jSGeneric;
                    //treeNode.Nodes.Add(jSObject2.Key);
                    
                    TreeNode treeNode2 = new TreeNode();
                    recursive_riempimento(jSObject2, ref treeNode2);
                    treeNode2.Name = "JSObject";
                    treeNode2.Text = jSObject2.Key;
                    treeNode.Nodes.Add(treeNode2);
                    
                    temp_count++;
                }
                if (jSGeneric is JSArray)
                {
                    JSArray jSArray = (JSArray)jSGeneric;
                    //treeNode.Nodes.Add(jSArray.Key);
                    
                    TreeNode treeNode2 = new TreeNode();
                    recursive_riempimentoNK(jSArray, ref treeNode2);
                    treeNode2.Name = "JSArray";
                    treeNode2.Text = jSArray.Key;
                    treeNode.Nodes.Add(treeNode2);
                    
                    temp_count++;
                }
            }
        }

        private void recursive_riempimentoNK(JSArray jSArray, ref TreeNode treeNode)
        {
            int temp_count = 0;
            foreach (JSGenericNK jSGenericNK in jSArray.Value)
            {
                if (jSGenericNK is JSBoolNK)
                {
                    JSBoolNK value = (JSBoolNK)jSGenericNK;
                    treeNode.Nodes.Add(value.Value.ToString());
                    treeNode.Nodes[temp_count].Name = "JSDoubleNKValue";
                    temp_count++;
                }
                if (jSGenericNK is JSDoubleNK)
                {
                    JSDoubleNK value = (JSDoubleNK)jSGenericNK;
                    treeNode.Nodes.Add(value.Value.ToString());
                    treeNode.Nodes[temp_count].Name = "JSDoubleNKValue";
                    temp_count++;
                }
                if (jSGenericNK is JSNullNK)
                {
                    JSNullNK value = (JSNullNK)jSGenericNK;
                    treeNode.Nodes.Add("null");
                    treeNode.Nodes[temp_count].Name = "JSNullNKValue";
                    temp_count++;
                }
                if (jSGenericNK is JSStringNK)
                {
                    JSStringNK value = (JSStringNK)jSGenericNK;
                    treeNode.Nodes.Add(value.Value);
                    //treeNode.Nodes[temp_count].Name = "JSStringNK";
                    treeNode.Nodes[temp_count].Name = "JSStringNKValue";
                    temp_count++;
                }
                if (jSGenericNK is JSObjectNK)
                {
                    JSObjectNK jSObject2 = (JSObjectNK)jSGenericNK;
                    //treeNode.Nodes.Add("jSObject");
                    TreeNode treeNode2 = new TreeNode();
                    JSObject jSObject3 = new JSObject();
                    jSObject3.Key = "JSObjectNK";
                    jSObject3.Value = jSObject2.Value;
                    recursive_riempimento(jSObject3, ref treeNode2);
                    treeNode2.Name = "JSObjectNK";
                    treeNode2.Text = "JSObjectNK";
                    treeNode.Nodes.Add(treeNode2);
                    temp_count++;
                }
                if (jSGenericNK is JSArrayNK)
                {
                    JSArrayNK jSArray2 = (JSArrayNK)jSGenericNK;
                    //treeNode.Nodes.Add("JSArray");
                    TreeNode treeNode2 = new TreeNode();
                    JSArray jSArray3 = new JSArray();
                    jSArray3.Key = "JSArrayNK";
                    jSArray3.Value = jSArray2.Value;
                    recursive_riempimentoNK(jSArray3, ref treeNode2);
                    treeNode2.Name = "JSArrayNK";
                    treeNode2.Text = "JSArrayNK";
                    treeNode.Nodes.Add(treeNode2);
                    temp_count++;
                }
            }
        }

        //Edit
        private void button1_Click(object sender, EventArgs e)
        {
            //treeView1.SelectedNode.Name = textBox1.Text;
            treeView1.SelectedNode.Text = textBox1.Text;
        }

        //Insert
        private void button2_Click(object sender, EventArgs e)
        {
            //treeView1.SelectedNode.Nodes.Add("NewNode");

            TreeNode node = treeView1.SelectedNode;
            //TreeNode node = nodes[0];

            using (SelectDataType selectDataType = new SelectDataType()) 
            {
                selectDataType.ShowDialog();
                TreeNode tempNode = new TreeNode();
                TreeNode tempNode2 = new TreeNode();
                if (node.Name.Equals("JSObject") || node.Name.Equals("JSObjectNK"))
                {
                    switch (selectDataType.buttonClicked)
                    {
                        case "string":
                            tempNode.Name = "JSString";
                            tempNode.Text = "NewStringKey";
                            tempNode2.Name = "JSStringValue";
                            tempNode2.Text = "NewStringValue";
                            tempNode.Nodes.Add(tempNode2);
                            break;
                        case "double":
                            tempNode.Name = "JSDouble";
                            tempNode.Text = "NewDoubleKey";
                            tempNode2.Name = "JSDoubleValue";
                            tempNode2.Text = "0";
                            tempNode.Nodes.Add(tempNode2);
                            break;
                        case "bool":
                            tempNode.Name = "JSBool";
                            tempNode.Text = "NewBoolValue";
                            tempNode2.Name = "JSBoolValue";
                            tempNode2.Text = "False";
                            tempNode.Nodes.Add(tempNode2);
                            break;
                        case "null":
                            tempNode.Name = "JSNull";
                            tempNode.Text = "NewNullKey";
                            tempNode2.Name = "JSNullValue";
                            tempNode2.Text = "null";
                            tempNode.Nodes.Add(tempNode2);
                            break;
                        case "object":
                            tempNode.Name = "JSObject";
                            tempNode.Text = "NewObjectKey";
                            break;
                        case "array":
                            tempNode.Name = "JSArray";
                            tempNode.Text = "NewArrayKey";
                            break;
                    }
                    treeView1.SelectedNode.Nodes.Add(tempNode);
                }
                else if (node.Name.Equals("JSArray") || node.Name.Equals("JSArrayNK"))
                {
                    switch (selectDataType.buttonClicked)
                    {
                        case "string":
                            tempNode.Name = "JSStringNK";
                            tempNode.Text = "NewStringValue";
                            break;
                        case "double":
                            tempNode.Name = "JSDoubleNK";
                            tempNode.Text = "0";
                            break;
                        case "bool":
                            tempNode.Name = "JSBoolNK";
                            tempNode.Text = "False";
                            break;
                        case "null":
                            tempNode.Name = "JSNullNK";
                            tempNode.Text = "null";
                            break;
                        case "object":
                            tempNode.Name = "JSObjectNK";
                            tempNode.Text = "JSObjectNK";
                            break;
                        case "array":
                            tempNode.Name = "JSArrayNK";
                            tempNode.Text = "JSArrayNK";
                            break;
                    }
                    treeView1.SelectedNode.Nodes.Add(tempNode);
                }
                else {
                    // Initializes the variables to pass to the MessageBox.Show method.
                    string message = "DataType can be inserted only in Object or Array!";
                    string caption = "Error Detected in Input";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        // Closes the parent form.
                        this.Close();
                    }
                }
                
            }
        }

        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            RemoveCheckedNode(treeView1.Nodes);
        }

        List<TreeNode> checkedNodes = new List<TreeNode>();
        private void RemoveCheckedNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked) { checkedNodes.Add(node); }
                else { RemoveCheckedNode(node.Nodes); }
            }

            foreach (TreeNode checkedNode in checkedNodes) { nodes.Remove(checkedNode); }
        }


        private JSObjectNK recursive_compose_obj(TreeNode nodes)
        {
            JSObjectNK jSObjectNK = new JSObjectNK();
            foreach (TreeNode node in nodes.Nodes)
            {
                switch (node.Name.ToString())
                {
                    case "JSString":
                        JSString jSString = new JSString();
                        jSString.Key = node.Text;
                        try { jSString.Value = node.Nodes[0].Text; }
                        catch (Exception) { jSString.Value = ""; }
                        jSObjectNK.Value.Add(jSString);
                        break;
                    case "JSBool":
                        JSBool jSBool = new JSBool();
                        jSBool.Key = node.Text;
                        try 
                        {
                            if (node.Nodes[0].Text.Equals("True") ||
                            node.Nodes[0].Text.Equals("true") ||
                            node.Nodes[0].Text.Equals("1"))
                            {
                                jSBool.Value = true;
                            }
                            else
                            {
                                jSBool.Value = false;
                            }
                        }
                        catch (Exception) { jSBool.Value = false; }
                        jSObjectNK.Value.Add(jSBool);
                        break;
                    case "JSDouble":
                        JSDouble jSDouble = new JSDouble();
                        jSDouble.Key = node.Text;
                        double tempDouble;
                        if (double.TryParse(node.Nodes[0].Text, out tempDouble))
                        { jSDouble.Value = tempDouble; }
                        else { jSDouble.Value = -1; }
                        try 
                        {
                            if (double.TryParse(node.Nodes[0].Text, out tempDouble))
                            { jSDouble.Value = tempDouble; }
                            else { jSDouble.Value = -1; }
                        }
                        catch (Exception) { jSDouble.Value = -1; }
                        jSObjectNK.Value.Add(jSDouble);
                        break;
                    case "JSNull":
                        JSNull jSNull = new JSNull();
                        jSNull.Key = node.Text;
                        jSObjectNK.Value.Add(jSNull);
                        break;
                    case "JSArray":
                        JSArray jSArray = new JSArray();
                        jSArray.Key = node.Text;
                        jSArray.Value = recursive_compose_array(node).Value;
                        jSObjectNK.Value.Add(jSArray);
                        break;
                    case "JSObject":
                        JSObject temp_jSObject = new JSObject();
                        temp_jSObject.Key = node.Text;
                        temp_jSObject.Value = recursive_compose_obj(node).Value;
                        jSObjectNK.Value.Add(temp_jSObject);
                        break;
                }
            }
            return jSObjectNK;
        }

        private JSArrayNK recursive_compose_array(TreeNode nodes)
        {
            JSArrayNK jSArrayNK = new JSArrayNK();
            foreach (TreeNode node in nodes.Nodes)
            {
                switch (node.Name.ToString())
                {
                    case "JSStringNK":
                        JSStringNK jSStringNK = new JSStringNK();
                        jSStringNK.Value = node.Text;
                        jSArrayNK.Value.Add(jSStringNK);
                        break;
                    case "JSBoolNK":
                        JSBoolNK jSBoolNK = new JSBoolNK();
                        if (node.Text.Equals("True") ||
                            node.Text.Equals("true") ||
                            node.Text.Equals("1"))
                        {
                            jSBoolNK.Value = true;
                        }
                        else
                        {
                            jSBoolNK.Value = false;
                        }
                        jSArrayNK.Value.Add(jSBoolNK);
                        break;
                    case "JSDoubleNK":
                        JSDoubleNK jSDoubleNK = new JSDoubleNK();
                        double tempDouble;
                        if (double.TryParse(node.Text, out tempDouble))
                        { jSDoubleNK.Value = tempDouble; }
                        else { jSDoubleNK.Value = -1; }
                        jSArrayNK.Value.Add(jSDoubleNK);
                        break;
                    case "JSNullNK":
                        JSNullNK jSNullNK = new JSNullNK();
                        jSArrayNK.Value.Add(jSNullNK);
                        break;
                    case "JSArrayNK":
                        JSArrayNK temp_jSArrayNK = new JSArrayNK();
                        temp_jSArrayNK.Value = recursive_compose_array(node).Value;
                        jSArrayNK.Value.Add(temp_jSArrayNK);
                        break;
                    case "JSObjectNK":
                        JSObjectNK temp_jSObject = new JSObjectNK();
                        temp_jSObject.Value = recursive_compose_obj(node).Value;
                        jSArrayNK.Value.Add(temp_jSObject);
                        break;
                }
            }
            return jSArrayNK;
        }

        //Move Up
        private void button5_Click(object sender, EventArgs e)
        {
            TreeNode parent = treeView1.SelectedNode.Parent;
            TreeNode temp = treeView1.SelectedNode;
            int index = treeView1.SelectedNode.Index;
            if (index > 0) 
            {
                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(index - 1, temp);
            }
            treeView1.SelectedNode = temp;
        }

        //Move Down
        private void button6_Click(object sender, EventArgs e)
        {
            TreeNode parent = treeView1.SelectedNode.Parent;
            TreeNode temp = treeView1.SelectedNode;
            int index = treeView1.SelectedNode.Index;
            if (index+1 < parent.Nodes.Count)
            {
                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(index + 1, temp);
            }
            treeView1.SelectedNode = temp;
        }

        //New File
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            resetTree();
        }

        private void resetTree() {
            treeView1.Nodes.Clear();
            TreeNode treeNode = new TreeNode();
            treeNode.Name = "JSObjectNK";
            treeNode.Text = "JsonFile";
            treeView1.Nodes.Add(treeNode);
            textBoxPath.Text = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + "\\NewJson.json";
        }

        //Open
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    textBoxPath.Text = file;
                    LoadJSStr(text);
                }
                catch (IOException){}
            }
        }

        private void LoadJSStr(string text)
        {
            JSDecoder decoder = new JSDecoder();
            JSObjectNK jSObjectNK = decoder.Decode(text);
            JSObject jSObject = new JSObject();
            jSObject.Key = "JsonFile";
            jSObject.Value = jSObjectNK.Value;

            TreeNode treeNode = new TreeNode();
            recursive_riempimento(jSObject, ref treeNode);
            treeView1.Nodes.Add(treeNode);
            treeNode.Name = "JSObjectNK";
            treeNode.Text = "JsonFile";
        }

        //Save
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SaveJSFile();
        }

        private void SaveJSFile()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            JSObjectNK jSObjectNK = new JSObjectNK();
            TreeNode node = nodes[0];
            jSObjectNK = recursive_compose_obj(node);
            JSEncoder jSEncoder = new JSEncoder();
            File.WriteAllText(textBoxPath.Text, jSEncoder.Encode(jSObjectNK));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //CreditForm
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            new CreditForm().ShowDialog();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            label1.Text = treeView1.SelectedNode.Name;
        }

    }
}
