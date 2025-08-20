/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        'gray-primary': '#D9D9D9',
        'lightgray-primary': '#F8F8F8',
        'dimgray-primary': '#737373',
        'blue-primary': '#548CFF',
        'transparent-blue-primary': '#548dff10',
        'blue-secondary': '#DBEAFE',
        'blue-black': '#0F0F30',
        'charcoal-blue': '#19193A',
        'darkblue-primary': '#314197',
        'darkblue-secondary': '#2C4A89',
        'lightblue-primary': '#F6F8FF',
        'lightblue-secondary': '#DEE9FF',
        'purple-primary': '#7900FF',
        'purple-secondary': '#5A78FF',
        'lightpurple-primary': '#8E6ECD',
        'toast-success-bg': '#EAFFDE',
        'toast-success-text': '#42B220',
        'green-primary': '#34A353',
        'red-primary': '#ED5E68',
        'red-secondary': '#FFDEDE',
        'red-alert': '#DC0032',
        'yellow-primary': '#FFA600',
        'yellow-secondary': '#FFE3AE',
        'golden-primary': '#EBA205',
        'golden-secondary': '#FFF4E9',
        'ups-yellow': '#FFCC04',
        'dpd-red': '#CB0032',
        'dhl-yellow': '#FFDD00',
        'fedex-purple': '#4D148C',
        'gradient-primary':
          'linear-gradient(270deg, var(--purple-primary) -117.67%, var(--blue-primary) 100%)',
        'green-gradient': 'linear-gradient(0deg, #95BF46 0%, #45C575 100%)'
      },
      backgroundImage: {
        'gradient-primary':
          'linear-gradient(270deg, var(--purple-primary) -117.67%, var(--blue-primary) 100%)',
        'green-gradient': 'linear-gradient(0deg, #95BF46 0%, #45C575 100%)',
        bg1: "url('/src/assets/animated-bg/bg1.svg')",
        noise: "url('/src/assets/animated-bg/noise.png')"
      },
      animation: {
        backgroundAnimation: 'backgroundAnimation 9s ease-in infinite'
      },
      keyframes: {
        backgroundAnimation: {
          '0%': {backgroundImage: "url('/src/assets/animated-bg/bg1.svg')"},
          '33%': {backgroundImage: "url('/src/assets/animated-bg/bg2.svg')"},
          '66%': {backgroundImage: "url('/src/assets/animated-bg/bg3.svg')"},
          '100%': {backgroundImage: "url('/src/assets/animated-bg/bg1.svg')"}
        }
      },
      fontFamily: {
        sequelsans: ['Sequel Sans', 'sans-serif']
      },
      transformOrigin: {
        center: '50% 50%'
      },
      rotate: {
        'y-90': 'rotateY(90deg)',
        'y-0': 'rotateY(0deg)'
      }
    }
  },
  plugins: [require('tailwindcss-primeui')]
}
