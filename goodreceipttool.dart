import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:keyencesatosbpl/loginform.dart';
class goodreceipttool extends StatefulWidget {
  const goodreceipttool({super.key});

  @override
  State<goodreceipttool> createState() => _goodreceipttool();
}

class _goodreceipttool extends State<goodreceipttool> {
  bool isChecked  = false;
  @override
  Widget build(BuildContext context) {
    final Size sizescreen = MediaQuery
        .of(context)
        .size;
    return Scaffold(
      appBar: AppBar(
        toolbarHeight: 40,
        title:  const Text('GR Tool',style: TextStyle(fontSize: 25)),
      ),
      body: SingleChildScrollView(
        child: Center(
          child: Container(
            padding: const EdgeInsets.only(top: 10),
            width: sizescreen.width * 0.95,
            height: sizescreen.height * 0.85,
            child: Column(

              children: [
                 const Row(
                  children: [
                    Expanded(
                      flex: 3,
                      child: Text('Barcode:',style: TextStyle(fontSize: 15,fontWeight: FontWeight.bold)),
                    ),
                    Expanded(
                      flex: 10,
                      //width: sizescreen.width * 0.7,
                      child: SizedBox(
                        height: 40,
                        child: TextField(
                          decoration: InputDecoration(
                            border: OutlineInputBorder(
                            ),
                            contentPadding: EdgeInsets.only(top: 10,bottom: 10,left: 5,right: 5),
                          ),
                          style: TextStyle(fontSize: 20),

                        ),
                      ),
                    ),

                  ],
                ),
                const SizedBox(height: 5,),
                Row(
                  children: [
                    const Expanded(
                      flex: 3,
                      child: Text('QtyActual:',style: TextStyle(fontSize: 15,fontWeight: FontWeight.bold)),
                    ),
                    //SizedBox(width: 2,),
                    const Expanded(
                      flex: 6,
                      child:  SizedBox(
                        height: 40,
                        child: TextField(
                          decoration: InputDecoration(
                            border: OutlineInputBorder(
                            ),
                            contentPadding: EdgeInsets.only(top: 0,bottom: 0,left: 5,right: 5),
                          ),
                          style: TextStyle(fontSize: 20,),

                        ),
                      ),
                    ),
                    const SizedBox(width: 5,),
                    Expanded(
                      flex: 4,
                      child: SizedBox(
                        height: 40,
                        child: ElevatedButton(
                          onPressed: (){

                          },
                          child: const Text('Finish',style: TextStyle(fontSize: 15)),
                        ),
                      )
                    )
                  ],
                ),
                const SizedBox(height: 5,),
                Row(
                  children: [
                    const Expanded(
                      flex: 5,
                      child: Center(child: Text('0 / 0',style: TextStyle(fontSize: 20,fontWeight: FontWeight.bold))),
                    ),
                    //SizedBox(width: 2,),
                    const SizedBox(width: 5,),
                    Expanded(
                      flex: 4,
                      child: InkWell(
                        onTap: () {
                          setState(() {
                            isChecked = !isChecked;
                          });
                        },
                        child: Row(
                          children: <Widget>[
                            Checkbox(
                              value: isChecked,
                              onChanged: ( newValue) {
                                setState(() {
                                  isChecked = newValue!;
                                });
                              },
                            ),
                            const Text('Hàng Thiếu',style: TextStyle(fontSize: 15)),
                          ],
                        ),
                      )
                    )
                  ],
                ),

                const Row(
                  children: [
                    Expanded(
                      flex: 2,
                      child: Text('Qty Invoice:',style: TextStyle(fontSize: 15,fontWeight: FontWeight.bold)),
                    ),
                    //SizedBox(width: 2,),
                    SizedBox(width: 5,),
                    Expanded(
                      flex: 6,
                      child: Center(child: Text('0/0',style: TextStyle(fontSize: 20,fontWeight: FontWeight.bold))),
                    )
                  ],
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    ElevatedButton(onPressed: (){

                    }, child: const Text('P StockCard')),
                    SizedBox(width: 15,),
                    ElevatedButton(onPressed: (){

                    }, child: const Text('P Panacim'))
                  ],
                ),

                Container(

                  width: sizescreen.width * 0.95,
                  color: Colors.green,
                  child: Text('Datatable'),
                )
              ],
            ),
          ),
        ),
      ),

    );
  }
}
