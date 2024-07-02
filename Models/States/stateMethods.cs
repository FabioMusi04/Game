using System.Drawing;
using System.Windows.Forms;

namespace GameProject.Services.States
{
  public interface StateMethods 
  {
    public void Update();
    public void Draw(Graphics g);
    public void MouseClick(object sender, MouseEventArgs e);
    public void MouseMove(object sender, MouseEventArgs e);
    public void MousePress(object sender, MouseEventArgs e);
    public void MouseRelease(object sender, MouseEventArgs e);
    public void KeyPress(object sender, KeyPressEventArgs e);
    public void KeyUp(object sender, KeyEventArgs e);
  }
}