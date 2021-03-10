# registerMachine

a small example registerMachine built in WPF 

## features 

- shows error the line errors occour
- has multiple registers named "eax", "ebx", "ecx", "edx", "cx"
- aviable orders: 
  - ADD: ex: ADD ebx equals eax + ebx and the result is in eax
  - SUB: same as ADD
  - MOV: ex: "MOV eax ebx" moves the value of ebx to eax
  - INC: ex: INC eax quals eax++
  - DEC: same as INC
  - MUL: ex: MUL ecx equals eax * ecx and the result is in eax
  - JMP: ex: JMP lineno jumps to a line in the code
  - LOOP: ex: LOOP lineno: cx times the code between loop and the lineno
  - LOG: Shows the text after the LOG in the consol
